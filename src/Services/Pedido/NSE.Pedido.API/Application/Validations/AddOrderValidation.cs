using FluentValidation;
using NSE.Pedido.API.Application.Commands;

namespace NSE.Pedido.API.Application.Validations
{
    public class AddOrderValidation : AbstractValidator<AddOrderCommand>
    {
        public AddOrderValidation()
        {
            RuleFor(addOrder => addOrder.OrderItems.Count)
                .GreaterThan(0)
                .WithMessage("O pedido precisa ter no minímo 1 item");

            RuleFor(addOrder => addOrder.Amount)
                .GreaterThan(0)
                .WithMessage("Valor do pedido inválido");

            RuleFor(addOrder => addOrder.CardNumber)
                .CreditCard()
                .WithMessage("Número do cartão inválido");

            RuleFor(addOrder => addOrder.Holder)
                .NotEmpty()
                .WithMessage("Nome do portador do cartão requerido.");

            RuleFor(addOrder => addOrder.SecurityCode.Length)
                .GreaterThan(2)
                .LessThan(5)
                .WithMessage("O CVV do cartão precisa ter 3 ou 4 números");

            RuleFor(addOrder => addOrder.ExpirationDate)
                .NotEmpty()
                .WithMessage("Data de expiração do cartão  requerida");
        }
    }
}
