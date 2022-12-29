using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.Identidade.API.Models;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class IdentidadeController : Controller
    {

        private readonly Interfaces.IAuthenticationService _authenticationService;

        public IdentidadeController(Interfaces.IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var response = await _authenticationService.Register(registerViewModel);

            // if (false) return View(registerViewModel);

            await LogInContext(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var response = await _authenticationService.Login(loginViewModel);

            // if (false) return View(loginViewModel);

            await LogInContext(response);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("sair")]
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }

        private async Task LogInContext(UserLoginResponse userLoginResponse)
        {

            var formattedToken = GetFormattedToken(userLoginResponse.AcessToken);

            var claimsPrincipal = ConfigureClaimsPrincipal(userLoginResponse.AcessToken, formattedToken.Claims);

            var authProperties = ConfigureAuthProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties
            );
        }

        private static JwtSecurityToken GetFormattedToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token) as JwtSecurityToken;
        }

        private static ClaimsPrincipal ConfigureClaimsPrincipal(string token, IEnumerable<Claim> tokenClaims)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim("JWT", token));
            claims.AddRange(tokenClaims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }

        private static AuthenticationProperties ConfigureAuthProperties()
        {
            return new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true,
            };
        }



    }
}