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

        public async Task<ValidationResult> RemoveCartItem(Guid productId)
        {
            var customerShoppingCart = await GetCustomerShoppingCart();
            var cartItemRef = await _shoppingCartRepository.FindCartItemBy(productId, customerShoppingCart.Id);

            await ValidateCartItem(productId, cartItemRef, customerShoppingCart);

            if (!ValidationResult.IsValid) return ValidationResult;

            return await PersistData(_shoppingCartRepository.UnitOfWork);
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

        public async Task<ValidationResult> UpdateCartItem(Guid productId, CartItem item)
        {
            var customerShoppingCart = await GetCustomerShoppingCart();
            var cartItemRef = await _shoppingCartRepository.FindCartItemBy(productId, customerShoppingCart.Id);

            await ValidateCartItem(productId, cartItemRef, customerShoppingCart, item);

            if (!ValidationResult.IsValid) return ValidationResult;

            customerShoppingCart.UpdateUnit(cartItemRef, item.Quantity);

            _shoppingCartRepository.UpdateCustomerShoppingCart(customerShoppingCart);
            _shoppingCartRepository.UpdateCartItem(cartItemRef);

            return await PersistData(_shoppingCartRepository.UnitOfWork);
        }

        private async Task ValidateCartItem(Guid productId, CartItem cartItemRef, CustomerShoppingCart customerShoppingCart, CartItem item = null)
        {
            var cartItemHasProduct = ValidationCartItemHasProduct(productId, item);
            var isCartNotNull = ValidateCartItemExists(item);
            var hasItemsInCart = customerShoppingCart.HasItem(cartItemRef);

            if (!cartItemHasProduct)
            {
                AddError("O item não corresponde ao informado");
            }
            if (!isCartNotNull)
            {
                AddError("Carrinho não encontrado");
            }

            if (cartItemRef is null || !hasItemsInCart)
            {
                AddError("O Item não etá no carrinho");
            }
        }

        private bool ValidationCartItemHasProduct(Guid productId, CartItem item)
        {
            return item != null && item.ProductId == productId;
        }

        private bool ValidateCartItemExists(CartItem item)
        {
            return item is not null;
        }
    }
}