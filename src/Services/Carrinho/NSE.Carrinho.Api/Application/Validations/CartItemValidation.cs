using FluentValidation;
using NSE.Carrinho.Api.Domain;

namespace NSE.Carrinho.Api.Application.Validations
{
    public class CartItemValidation : AbstractValidator<CartItem>
    {
        public CartItemValidation()
        {
            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage(item => $"A quantidade miníma para o {item.Name} é 1");

            RuleFor(c => c.Quantity)
                .LessThanOrEqualTo(CartItem.MAX_ITEMS)
                .WithMessage(item => $"A quantidade máxima para o {item.Name} é {CartItem.MAX_ITEMS}");

            RuleFor(c => c.Price)
                .GreaterThan(0)
                .WithMessage(item => $"O valor do {item.Name} precisa ser maior que 0");
        }

    }
}