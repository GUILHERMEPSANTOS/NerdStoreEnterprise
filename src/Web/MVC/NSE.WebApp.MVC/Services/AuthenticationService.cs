using System.Text;
using System.Text.Json;
using NSE.WebApp.MVC.Authentication;
using NSE.WebApp.MVC.Interfaces;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Models.Errors;

namespace NSE.WebApp.MVC.Services
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> Login(LoginViewModel loginViewModel)
        {

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5140/api/identidade/autenticar", loginViewModel);

            if (!HandleResponseError(response))
            {
                return await ReturnResponseError(response);
            }

            return await SerializeResponse<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(RegisterViewModel registerViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5140​/api​/identidade​/nova-conta", registerViewModel);

            if (!HandleResponseError(response))
            {
                return await ReturnResponseError(response);
            }

            return await SerializeResponse<UserLoginResponse>(response);
        }

        private async Task<UserLoginResponse> ReturnResponseError(HttpResponseMessage response)
        {
            return new UserLoginResponse
            {
                ResponseResult = await SerializeResponse<ResponseResult>(response)
            };
        }
    }
}