using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "o Campo {0} está em formato inv�lido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo {0} o obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conderem")]
        public string ConfirmPassword { get; set; }
    }
}