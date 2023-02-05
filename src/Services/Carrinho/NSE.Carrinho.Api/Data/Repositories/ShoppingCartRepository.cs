using Core.Data;
using Microsoft.EntityFrameworkCore;
using NSE.Carrinho.Api.Domain;
using NSE.Carrinho.Api.Domain.Interfaces;

namespace NSE.Carrinho.Api.Data.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShoppingCartContext _shoppingCartContext;
        public IUnitOfWork UnitOfWork => _shoppingCartContext;

        public ShoppingCartRepository(ShoppingCartContext shoppingCartContext)
        {
            _shoppingCartContext = shoppingCartContext;
        }

        public async Task<CustomerShoppingCart> GetCustomerShoppingCart(Guid customerId)
        {
            return await _shoppingCartContext.CustomerShoppingCarts
                .Include(shopCart => shopCart.Items)
                .AsSplitQuery()
                .FirstOrDefaultAsync(shopCart => shopCart.CustomerId == customerId);
        }

        public void AddCustomerShoppingCart(CustomerShoppingCart customerShoppingCart)
        {
            _shoppingCartContext.CustomerShoppingCarts.Add(customerShoppingCart);
        }

        public void UpdateCustomerShoppingCart(CustomerShoppingCart customerShoppingCart)
        {
            _shoppingCartContext.CustomerShoppingCarts.Update(customerShoppingCart);
        }

        public void AddCartItem(CartItem cartItem)
        {
            _shoppingCartContext.CartItems.Add(cartItem);
        }

        public void UpdateCartItem(CartItem cartItem)
        {
            _shoppingCartContext.CartItems.Update(cartItem);
        }

        public async Task<CartItem> FindCartItemBy(Guid productId, Guid ShoppingCartId)
        {
            return await _shoppingCartContext
                .CartItems.FirstOrDefaultAsync(cartItem => cartItem.ShoppingCartId == ShoppingCartId && cartItem.ProductId == productId);
        }
    }
}