using Microsoft.AspNetCore.Mvc;
using NSE.Carrinho.Api.Application.Interfaces;
using NSE.Carrinho.Api.Domain;
using NSE.WebApi.Core.Controllers;

namespace NSE.Carrinho.Api.Controllers
{
    [Route("carrinho")]
    public class ShoppingCartController : MainController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingCart()
        {
            var result = await _shoppingCartService.GetCustomerShoppingCart();

            return CustomResponse(result ?? new CustomerShoppingCart());
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart(CartItem item)
        {
            var result = await _shoppingCartService.AddCartItem(item);

            return CustomResponse(result);
        }
    }
}