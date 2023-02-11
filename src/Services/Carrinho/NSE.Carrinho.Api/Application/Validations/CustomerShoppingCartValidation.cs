using FluentValidation;
using NSE.Carrinho.Api.Domain;

namespace NSE.Carrinho.Api.Application.Validations
{
    public class CustomerShoppingCartValidation : AbstractValidator<CustomerShoppingCart>
    {
        public CustomerShoppingCartValidation()
        {
            RuleFor(cart => cart.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Cliente não reconhecido");

            RuleFor(cart => cart.Items.Count)
                .GreaterThan(0)
                .WithMessage("O carrinho não possui itens");

            RuleFor(cart => cart.Total)
                .GreaterThan(0)
                .WithMessage("O valor total do carrinho precisa ser maior que 0");
        }
    }
}