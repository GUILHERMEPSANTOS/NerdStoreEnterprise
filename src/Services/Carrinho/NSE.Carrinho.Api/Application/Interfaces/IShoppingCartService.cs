using FluentValidation.Results;
using NSE.Carrinho.Api.Domain;

namespace NSE.Carrinho.Api.Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<CustomerShoppingCart> GetCustomerShoppingCart();

        Task<ValidationResult> AddCartItem(CartItem item);
    }
}