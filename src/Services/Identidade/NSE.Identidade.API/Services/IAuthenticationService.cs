

using Microsoft.AspNetCore.Identity;
using NSE.Identidade.API.Models;
using NSE.Identidade.API.Models.Token;

namespace NSE.Identidade.API.Services
{
    public interface IAuthenticationService
    {
        SignInManager<IdentityUser> SignInManager { get; }
        UserManager<IdentityUser> UserManager { get; }
        Task<UserLoginResponse> GerarJWT(string email);
        Task<RefreshToken> GetRefreshTokenAsync(Guid token);
    }
}