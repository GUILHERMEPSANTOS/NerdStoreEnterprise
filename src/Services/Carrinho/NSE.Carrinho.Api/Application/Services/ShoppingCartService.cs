using NSE.Carrinho.Api.Domain;
using NSE.Carrinho.Api.Domain.Interfaces;
using NSE.Carrinho.Api.Application.Interfaces;
using NSE.WebApi.Core.User;
using FluentValidation.Results;

namespace NSE.Carrinho.Api.Application.Services
{
    public class ShoppingCartService : ServiceBase, IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IAspNetUser _aspNetUser;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IAspNetUser aspNetUser)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _aspNetUser = aspNetUser;
        }

        public async Task<ValidationResult> AddCartItem(CartItem item)
        {
            var shoppingCart = await GetCustomerShoppingCart();

            if (shoppingCart is null)
            {
                HandleNewCart(item);
            }
            else
            {
                HandleExistingCart(shoppingCart, item);
            }

            return await PersistData(_shoppingCartRepository.UnitOfWork);
        }

        private void HandleNewCart(CartItem cartItem)
        {
            var shoppingCart = new CustomerShoppingCart(_aspNetUser.GetUserId());

            shoppingCart.AddItem(cartItem);

            _shoppingCartRepository.AddCustomerShoppingCart(shoppingCart);

        }

        private void HandleExistingCart(CustomerShoppingCart customerShoppingCart, CartItem cartItem)
        {
            var itemAlreadyExistsInCart = customerShoppingCart.HasItem(cartItem);

            customerShoppingCart.AddItem(cartItem);

            if (itemAlreadyExistsInCart)
            {
                _shoppingCartRepository.UpdateCartItem(customerShoppingCart.GetCartItemBy(cartItem.Id));
            }
            else
            {
                _shoppingCartRepository.AddCartItem(cartItem);
            }

            _shoppingCartRepository.UpdateCustomerShoppingCart(customerShoppingCart);

        }
        public async Task<CustomerShoppingCart> GetCustomerShoppingCart()
        {
            return await _shoppingCartRepository.GetCustomerShoppingCart(_aspNetUser.GetUserId());
        }
    }
}