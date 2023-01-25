using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Identidade.API.Models;
using NSE.WebApi.Core.Extensions;
using NSE.WebApi.Core.Controllers;
using Core.Messages.Integration;
using EasyNetQ;

namespace NSE.Identidade.API.Controllers
{

    [Route("api/identidade")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;
        private IBus _bus;
        
        public AuthController(SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("nova-conta")]
        public async Task<IActionResult> CreateUser([FromBody] NewUser newUser)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = new IdentityUser()
            {
                UserName = newUser.Email,
                Email = newUser.Email,
                EmailConfirmed = true,
            };


            var result = await _userManager.CreateAsync(user, newUser.Password);


            if (result.Succeeded)
            {
                var success = await RegisterUser(newUser);

                return CustomResponse(await GerarJWT(user.Email));
            }

            foreach (var error in result.Errors)
            {
                AddErrorsProcessing(error.Description);
            }

            return CustomResponse();
        }
        private async Task<ResponseMessage> RegisterUser(NewUser newUser)
        {
            var user = await _userManager.FindByEmailAsync(newUser.Email);

            var userRegistered = new UserRegisteredIntegrationEvent(
                Guid.Parse(user.Id),
                newUser.Name,
                newUser.Email,
                newUser.Cpf
            );

            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            var success = await _bus.Rpc.RequestAsync<UserRegisteredIntegrationEvent, ResponseMessage>(userRegistered);

            return success;
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager
                                     .PasswordSignInAsync(loginModel.Email, loginModel.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return Ok(await GerarJWT(loginModel.Email));
            }

            if (result.IsLockedOut)
            {
                AddErrorsProcessing("Usuário temprariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AddErrorsProcessing("Usuário ou Senha incorretos");
            return CustomResponse();

        }

        private async Task<UserLoginResponse> GerarJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ConfigureUserClaims(claims, user);

            var encodedToken = EncodeToken(identityClaims);

            return GetResponseToken(encodedToken, user, claims);

        }

        private async Task<ClaimsIdentity> ConfigureUserClaims(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            return new ClaimsIdentity(claims);
        }
        private string EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandle = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandle.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiresIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandle.WriteToken(token);
        }
        private UserLoginResponse GetResponseToken(string encodedToken, IdentityUser user, ICollection<Claim> claims)
        {
            return new UserLoginResponse()
            {
                AcessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiresIn).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
