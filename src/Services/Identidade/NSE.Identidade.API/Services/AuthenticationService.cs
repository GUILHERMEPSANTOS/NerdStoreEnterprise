using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using NSE.Identidade.API.Data;
using NSE.Identidade.API.Extensions;
using NSE.Identidade.API.Models;
using NSE.Identidade.API.Models.Token;
using NSE.WebApi.Core.User;

namespace NSE.Identidade.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public SignInManager<IdentityUser> SignInManager => _signInManager;
        public UserManager<IdentityUser> UserManager => _userManager;
        private readonly AppTokenSettings _appTokenSettings;
        private readonly IJwtService _jwtService;
        private readonly IAspNetUser _aspNetUser;
        private readonly ApplicationDbContext _context;

        public AuthenticationService(SignInManager<IdentityUser> signInManager,
                                     UserManager<IdentityUser> userManager,
                                     IOptions<AppTokenSettings> appTokenSettings,
                                     IJwtService jwtService,
                                     IAspNetUser aspNetUser,
                                     ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appTokenSettings = appTokenSettings.Value;
            _jwtService = jwtService;
            _aspNetUser = aspNetUser;
            _context = context;
        }

        public async Task<UserLoginResponse> GerarJWT(string email)
        {

            var user = await UserManager.FindByEmailAsync(email);
            var claims = await UserManager.GetClaimsAsync(user);

            var identityClaims = await ConfigureUserClaims(claims, user);
            var encodedToken = await EncodeToken(identityClaims);
            var refreshToken = await GenerateRefreshToken(email);

            return GetResponseToken(encodedToken, user, claims, refreshToken);
        }

        private async Task<ClaimsIdentity> ConfigureUserClaims(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);

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
        private async Task<string> EncodeToken(ClaimsIdentity identityClaims)
        {
            var tokenHandle = new JwtSecurityTokenHandler();

            var key = await _jwtService.GetCurrentSigningCredentials();

            var descriptor = new SecurityTokenDescriptor()
            {
                Issuer = $"{_aspNetUser.GetHttpContext().Request.Scheme}://{_aspNetUser.GetHttpContext().Request.Host}",
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = key
            };

            var token = tokenHandle.CreateToken(descriptor);

            return tokenHandle.WriteToken(token);
        }
        private UserLoginResponse GetResponseToken(string encodedToken, IdentityUser user, ICollection<Claim> claims, RefreshToken token)
        {
            return new UserLoginResponse()
            {
                AcessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
                RefreshToken = token.Token,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private async Task<RefreshToken> GenerateRefreshToken(string email)
        {
            var refreshToken = new RefreshToken
            {
                UserName = email,
                ExpirationDate = DateTime.UtcNow.AddHours(_appTokenSettings.RefreshTokenExpiration),
            };

            var refreshTokensByUser = await _context
                .RefreshTokens
                .Where(refreshToken => refreshToken.UserName == email)
                .ToListAsync();

            _context.RefreshTokens.RemoveRange(refreshTokensByUser);

            await _context.RefreshTokens.AddAsync(refreshToken);

            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(Guid token)
        {
            var refreshToken = await _context.RefreshTokens.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Token == token);

            var expirationDateLocalTime = refreshToken?.ExpirationDate.ToLocalTime();

            var isExpired = expirationDateLocalTime < DateTime.Now;

            return refreshToken is null || isExpired ? null : refreshToken;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}