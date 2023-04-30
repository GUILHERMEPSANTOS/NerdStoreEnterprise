using FluentValidation;
using NSE.Cliente.API.Application.Customer.Commands;

namespace NSE.Cliente.API.Application.Customer.Validations
{
    public class AddAddressValidation : AbstractValidator<AddAddressCommand>
    {
        public AddAddressValidation()
        {
            RuleFor(c => c.StreetAddress)
                   .NotEmpty()
                   .WithMessage("informe o logradouro");

            RuleFor(c => c.BuildingNumber)
                .NotEmpty()
                .WithMessage("Informe o número");

            RuleFor(c => c.ZipCode)
                .NotEmpty()
                .WithMessage("Informe o CEP");

            RuleFor(c => c.Neighborhood)
                .NotEmpty()
                .WithMessage("Informe o bairro");

            RuleFor(c => c.City)
                .NotEmpty()
                .WithMessage("Informe a cidade");

            RuleFor(c => c.State)
                .NotEmpty()
                .WithMessage("Informe o Estado");
        }
    }
}