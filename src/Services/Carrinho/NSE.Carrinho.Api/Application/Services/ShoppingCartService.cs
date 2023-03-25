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

        public async Task<ValidationResult> UpdateCartItem(Guid productId, CartItem item)
        {
            var customerShoppingCart = await GetCustomerShoppingCart();
            var cartItemRef = await _shoppingCartRepository.FindCartItemBy(productId, customerShoppingCart.Id);

            await ValidateCartItem(productId, cartItemRef, customerShoppingCart, item);

            if (!ValidationResult.IsValid) return ValidationResult;

            customerShoppingCart.UpdateUnit(cartItemRef, item.Quantity);

            ValidateShoppingCart(customerShoppingCart);

            if (!cartItemRef.IsValid()) return ValidationResult;

            _shoppingCartRepository.UpdateCustomerShoppingCart(customerShoppingCart);
            _shoppingCartRepository.UpdateCartItem(cartItemRef);

            return await PersistData(_shoppingCartRepository.UnitOfWork);
        }

        public async Task<ValidationResult> RemoveCartItem(Guid productId)
        {
            var customerShoppingCart = await GetCustomerShoppingCart();
            var cartItemRef = await _shoppingCartRepository.FindCartItemBy(productId, customerShoppingCart.Id);

            await ValidateCartItem(productId, cartItemRef, customerShoppingCart);
            ValidateShoppingCart(customerShoppingCart);

            if (!ValidationResult.IsValid) return ValidationResult;

            customerShoppingCart.RemoveItem(cartItemRef);

            _shoppingCartRepository.UpdateCustomerShoppingCart(customerShoppingCart);
            _shoppingCartRepository.RemoveCartItem(cartItemRef);

            return await PersistData(_shoppingCartRepository.UnitOfWork);
        }

        public async Task<ValidationResult> ApplyVoucher(Voucher voucher)
        {
            var customerShoppingCart = await GetCustomerShoppingCart();

            customerShoppingCart.ApplyVoucher(voucher);

            _shoppingCartRepository.UpdateCustomerShoppingCart(customerShoppingCart);

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

            if (!ValidationResult.IsValid) return ValidationResult;

            return await PersistData(_shoppingCartRepository.UnitOfWork);
        }

        private void HandleNewCart(CartItem cartItem)
        {
            var shoppingCart = new CustomerShoppingCart(_aspNetUser.GetUserId());

            shoppingCart.AddItem(cartItem);

            ValidateShoppingCart(shoppingCart);

            _shoppingCartRepository.AddCustomerShoppingCart(shoppingCart);

        }

        private void HandleExistingCart(CustomerShoppingCart customerShoppingCart, CartItem cartItem)
        {
            var itemAlreadyExistsInCart = customerShoppingCart.HasItem(cartItem);
            customerShoppingCart.AddItem(cartItem);

            ValidateShoppingCart(customerShoppingCart);

            if (itemAlreadyExistsInCart)
            {
                _shoppingCartRepository.UpdateCartItem(customerShoppingCart.GetCartItemBy(cartItem.ProductId));
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


        private async Task ValidateCartItem(Guid productId, CartItem cartItemRef, CustomerShoppingCart customerShoppingCart, CartItem item = null)
        {
            var cartItemHasProduct = ValidationCartItemHasProduct(productId, item);
            var isShoppingCartNull = ValidateShoppingCartExists(customerShoppingCart);
            var hasItemsInCart = customerShoppingCart.HasItem(cartItemRef);

            if (cartItemHasProduct)
            {
                AddError("O item não corresponde ao informado");
            }
            if (isShoppingCartNull)
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
            return item != null && item.ProductId != productId;
        }

        private bool ValidateShoppingCartExists(CustomerShoppingCart customerShoppingCart)
        {
            return customerShoppingCart is null;
        }
        private bool ValidateShoppingCart(CustomerShoppingCart cart)
        {
            if (cart.IsValid()) return true;

            cart.ValidationResult.Errors.ForEach(error => AddError(error.ErrorMessage));

            return false;
        }
    }
}