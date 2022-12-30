

using NSE.WebApp.MVC.Authentication;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> Register(RegisterViewModel registerViewModel);
        Task<UserLoginResponse> Login(LoginViewModel loginViewModel);
    }
}