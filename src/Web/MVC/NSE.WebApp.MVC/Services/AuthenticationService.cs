using System.Text;
using System.Text.Json;
using NSE.Identidade.API.Models;
using NSE.WebApp.MVC.Interfaces;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserLoginResponse> Login(LoginViewModel loginViewModel)
        {

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5140/api/identidade/autenticar", loginViewModel);


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };


            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserLoginResponse> Register(RegisterViewModel registerViewModel)
        {

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5140​/api​/identidade​/nova-conta", registerViewModel);


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };


            return JsonSerializer.Deserialize<UserLoginResponse>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}