using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O Campo {0} o obrigatório")]
        [EmailAddress(ErrorMessage = "o Campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo {0} o obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }
    }
}