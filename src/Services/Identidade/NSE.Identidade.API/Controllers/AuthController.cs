using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Controllers
{

    [ApiController]
    [Route("api/identidade")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("nova-conta")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser()
            {
                UserName = registerModel.Email,
                Email = registerModel.Email,
                EmailConfirmed = true,
            };


            var result = await _userManager.CreateAsync(user, registerModel.Password);


            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager
                                     .PasswordSignInAsync(loginModel.Email, loginModel.Password, isPersistent: false, lockoutOnFailure: true);


            if (result.Succeeded)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        // public async Task<UsuarioRespostaLogin> GerarJWT()
        // {

        // }
    }
}
