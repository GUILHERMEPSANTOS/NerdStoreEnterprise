using Core.Data;

namespace NSE.Carrinho.Api.Domain.Interfaces
{
    public interface IShoppingCartRepository
    {
        IUnitOfWork UnitOfWork { get; }
        Task<CustomerShoppingCart> GetCustomerShoppingCart(Guid customerId);
        void AddCustomerShoppingCart(CustomerShoppingCart customerShoppingCart);
        void UpdateCustomerShoppingCart(CustomerShoppingCart customerShoppingCart);
        void AddCartItem(CartItem cartItem);
        void UpdateCartItem(CartItem cartItem);

        
    }
}