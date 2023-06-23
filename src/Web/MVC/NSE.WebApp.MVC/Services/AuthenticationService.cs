using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Communication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using NSE.WebApi.Core.User;
using NSE.WebApp.MVC.Authentication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class AuthenticationService : ServiceBase, MVC.Interfaces.IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAspNetUser _aspNetUser;

        public AuthenticationService(HttpClient httpClient
                                    ,IOptions<AppSettings> settings                                
                                    ,IAspNetUser aspNetUser
                                    ,IHttpContextAccessor httpContextAccessor)
        {

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl);
            _aspNetUser = aspNetUser;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserLoginResponse> Login(LoginViewModel loginViewModel)
        {

            var response = await _httpClient.PostAsJsonAsync("/api/identidade/autenticar", loginViewModel);

            if (!HandleResponseError(response))
            {
                return await ReturnResponseError(response);
            }

            return await DeserializeResponse<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(RegisterViewModel registerViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/identidade/nova-conta", registerViewModel);

            if (!HandleResponseError(response))
            {
                return await ReturnResponseError(response);
            }

            return await DeserializeResponse<UserLoginResponse>(response);
        }

        private async Task<UserLoginResponse> UseRefreshToken(string refreshToken)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/identidade/refresh-token", new { RefreshToken = refreshToken });

            if (!HandleResponseError(response))
            {
                return await ReturnResponseError(response);
            }

            return await DeserializeResponse<UserLoginResponse>(response);
        }

        private async Task<UserLoginResponse> ReturnResponseError(HttpResponseMessage response)
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeResponse<ResponseResult>(response)
            };
        }
        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme, null);
        }
        
        public async Task<bool> ValidRefreshToken()
        {
            var refreshToken = _aspNetUser.GetUserRefreshToken();
            var newToken = await UseRefreshToken(refreshToken);
            var isValidToken = newToken.IsValidToken();

            if (isValidToken)
            {
                await LogInContext(newToken);
                return true;
            }

            return false;
        }
        public bool ExpiredToken()
        {
            var token = _aspNetUser.GetUserToken();

            if (string.IsNullOrEmpty(token)) return false;

            var jwt = GetFormattedToken(token);

            return jwt.ValidTo.ToLocalTime() < DateTime.Now;
        }

        public async Task LogInContext(UserLoginResponse userLoginResponse)
        {

            var httpContext = _aspNetUser.GetHttpContext();
            var formattedToken = GetFormattedToken(userLoginResponse.AcessToken);
            var claimsPrincipal = ConfigureClaimsPrincipal(userLoginResponse, formattedToken.Claims);
            var authProperties = ConfigureAuthProperties();

            await _httpContextAccessor.HttpContext!.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties
            );
        }

        private JwtSecurityToken GetFormattedToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token) as JwtSecurityToken;
        }

        private ClaimsPrincipal ConfigureClaimsPrincipal(UserLoginResponse userLoginResponse, IEnumerable<Claim> tokenClaims)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim("JWT", userLoginResponse.AcessToken));
            claims.Add(new Claim("RefreshToken", userLoginResponse.RefreshToken));
            claims.AddRange(tokenClaims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }

        private AuthenticationProperties ConfigureAuthProperties()
        {
            return new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                IsPersistent = true,
            };
        }
    }
}