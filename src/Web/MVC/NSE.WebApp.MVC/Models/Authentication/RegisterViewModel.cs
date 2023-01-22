using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [DisplayName("CPF")]
        [Cpf]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "o Campo {0} está em formato inv�lido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Campo {0} o obrigatório")]
        [StringLength(100, ErrorMessage = "O Campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        [DisplayName("Senha")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conderem")]
        [DisplayName("Confirmar Senha")]
        public string ConfirmPassword { get; set; }
    }
}