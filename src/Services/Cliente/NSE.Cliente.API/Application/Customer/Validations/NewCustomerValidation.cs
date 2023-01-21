using FluentValidation;
using NSE.Cliente.API.Application.Customer.Commands;

namespace NSE.Cliente.API.Application.Customer.Validations
{
    public class NewCustomerValidation : AbstractValidator<NewCustomerCommand>
    {
        public NewCustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("id do cliente inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado");

            RuleFor(c => c.Cpf)
                .Must(IsValidCpf)
                .WithMessage("O CPF informado não é válido");

            RuleFor(c => c.Email)
                .Must(HasValidEmail)
                .WithMessage("O e-mail informado não é válido");
        }

        protected static bool IsValidCpf(string cpf) 
        {
            return Core.DomainObjects.Cpf.Validate(cpf);
        }

        protected static bool HasValidEmail(string email)
        {
            return Core.DomainObjects.Email.Validate(email);
        }
    }
}