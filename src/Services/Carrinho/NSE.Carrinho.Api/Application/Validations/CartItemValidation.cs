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
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantity)
                .LessThan(CartItem.MAX_ITEMS)
                .WithMessage($"A quantidade máxima de um item é {CartItem.MAX_ITEMS}");

            RuleFor(c => c.Price)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }

    }
}