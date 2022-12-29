using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.Identidade.API.Models;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> Register(RegisterViewModel registerViewModel);
        Task<UserLoginResponse> Login(LoginViewModel loginViewModel);
    }
}