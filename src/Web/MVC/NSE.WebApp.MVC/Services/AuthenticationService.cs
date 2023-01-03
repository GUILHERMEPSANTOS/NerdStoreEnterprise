using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Authentication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Interfaces;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Models.Errors;

namespace NSE.WebApp.MVC.Services
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient, IOptions<AppSettings> settings)
        {

            _httpClient = httpClient;
             _httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl);
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

        private async Task<UserLoginResponse> ReturnResponseError(HttpResponseMessage response)
        {
            return new UserLoginResponse
            {
                ResponseResult = await DeserializeResponse<ResponseResult>(response)
            };
        }
    }
}