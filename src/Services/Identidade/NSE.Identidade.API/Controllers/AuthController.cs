using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NSE.Identidade.API.Models;
using NSE.WebApi.Core.Controllers;
using Core.Messages.Integration;
using NSE.MessageBus;
using NSE.Identidade.API.Services;
using Microsoft.AspNetCore.Identity;
using NSE.Identidade.API.Models.Token;
using NSE.Identidade.API.DTO;

namespace NSE.Identidade.API.Controllers
{

    [Route("api/identidade")]
    public class AuthController : MainController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMessageBus _bus;

        public AuthController(IAuthenticationService authenticationService, IMessageBus bus)
        {
            _authenticationService = authenticationService;
            _bus = bus;
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

            var result = await _authenticationService.UserManager.CreateAsync(user, newUser.Password);

            if (result.Succeeded)
            {
                var clienteResult = await RegisterUser(newUser);

                if (!clienteResult.ValidationResult.IsValid)
                {
                    await _authenticationService.UserManager.DeleteAsync(user);

                    return CustomResponse(clienteResult.ValidationResult);
                }

                return CustomResponse(await _authenticationService.GerarJWT(user.Email));
            }

            foreach (var error in result.Errors)
            {
                AddErrorsProcessing(error.Description);
            }

            return CustomResponse();
        }
        private async Task<ResponseMessage> RegisterUser(NewUser newUser)
        {
            var user = await _authenticationService.UserManager.FindByEmailAsync(newUser.Email);

            var userRegistered = new UserRegisteredIntegrationEvent(
                Guid.Parse(user.Id),
                newUser.Name,
                newUser.Email,
                newUser.Cpf
            );

            try
            {
                return await _bus.RequestAsync<UserRegisteredIntegrationEvent, ResponseMessage>(userRegistered);
            }
            catch (Exception)
            {
                await _authenticationService.UserManager.DeleteAsync(user);
                throw;
            }
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authenticationService.SignInManager
                                     .PasswordSignInAsync(loginModel.Email, loginModel.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                return Ok(await _authenticationService.GerarJWT(loginModel.Email));
            }

            if (result.IsLockedOut)
            {
                AddErrorsProcessing("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse();
            }

            AddErrorsProcessing("Usuário ou Senha incorretos");
            return CustomResponse();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken.RefreshToken))
            {
                AddErrorsProcessing("Refresh Token inválido");
                return CustomResponse();
            }

            var token = await _authenticationService.GetRefreshTokenAsync(Guid.Parse(refreshToken.RefreshToken));

            if (token is null)
            {
                AddErrorsProcessing("Refresh Token expirado");
                return CustomResponse();
            }

            var newToken = await _authenticationService.GerarJWT(token.UserName);

            return CustomResponse(newToken);
        }
    }
}