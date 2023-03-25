using FluentValidation.Results;
using NSE.Carrinho.Api.Domain;

namespace NSE.Carrinho.Api.Application.Interfaces
{
    public interface IShoppingCartService
    {
        Task<CustomerShoppingCart> GetCustomerShoppingCart();
        Task<ValidationResult> AddCartItem(CartItem item);
        Task<ValidationResult> ApplyVoucher(Voucher voucher);
        Task<ValidationResult> UpdateCartItem(Guid productId, CartItem item);
        Task<ValidationResult> RemoveCartItem(Guid productId);
    }
}