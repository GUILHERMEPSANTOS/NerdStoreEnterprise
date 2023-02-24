using Microsoft.AspNetCore.Mvc;
using NSE.Bff.Compras.DTOs;
using NSE.Bff.Compras.Services.Interfaces;
using NSE.WebApi.Core.Controllers;

namespace NSE.Bff.Compras.Controllers
{
    [Route("compras")]
    public class ShoppingCartController : MainController
    {
        private readonly ICatalogService _catalogService;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(ICatalogService catalogService, IShoppingCartService shoppingCartService)
        {
            _catalogService = catalogService;
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("carrinho")]
        public async Task<IActionResult> GetShoppingCart()
        {
            return CustomResponse(await _shoppingCartService.GetShoppingCart());
        }

        [HttpGet("carrinho-quantidade")]
        public async Task<int> GetShoppingCartItemsQuantity()
        {
            var shoppingCart = await _shoppingCartService.GetShoppingCart();
            var quantity = shoppingCart?.Items.Sum(item => item.Quantity);

            return quantity ?? 0;
        }

        [HttpPost("carrinho-adicionar")]
        public async Task<IActionResult> AddCartItem([FromBody] CartItemDTO item)
        {
            var product = await _catalogService.GetById(item.ProductId);

            await ValidateShoppingCartItem(product: product, quantity: item.Quantity);

            if (ValidOperation())
            {
                item.Name = product.Name;
                item.Image = product.Image;
                item.Price = product.Price;

                var result = await _shoppingCartService.AddCartItem(item);
                return CustomResponse(result);
            }

            return CustomResponse();
        }

        [HttpPut("carrinho-atualizar/{productId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, CartItemDTO item)
        {
            var product = await _catalogService.GetById(productId);

            await ValidateShoppingCartItem(product: product, quantity: item.Quantity);

            if (ValidOperation())
            {
                var result = await _shoppingCartService.UpdateCartItem(productId: productId, item: item);

                return CustomResponse(result);
            }

            return CustomResponse();
        }

        [HttpDelete("carrinho-remover/{productId}")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var product = await _catalogService.GetById(productId);

            if (product is null)
            {
                AddErrorsProcessing("Produto inválido");
                return CustomResponse();
            }

            var result = await _shoppingCartService.RemoveCartItem(productId: productId);

            return CustomResponse(result);
        }

        private async Task ValidateShoppingCartItem(ProductDTO product, int quantity)
        {
            if (product is null) AddErrorsProcessing("Produto inexistente");
            if (quantity < 1) AddErrorsProcessing($"Escolha ao menos uma unidade do produto {product.Name}");

            var hasErrorInShoppingCart = await ValidateShoppingCart(quantity, product);

            if (hasErrorInShoppingCart) return;

            if (quantity > product.StockQuantity) AddErrorsProcessing($"O Produto {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");

        }

        private async Task<bool> ValidateShoppingCart(int quantity, ProductDTO product)
        {
            var shoppingCart = await _shoppingCartService.GetShoppingCart();
            var cartItem = shoppingCart?.Items.FirstOrDefault(item => item.ProductId == product.Id) ?? new CartItemDTO();
            var totalQuantity = quantity + (cartItem?.Quantity ?? 0);

            if (shoppingCart is not null && totalQuantity > product.StockQuantity)
            {
                AddErrorsProcessing($"O produto {product.Name} possui {product.StockQuantity} unidades d eestoque, você selecionou {quantity}");
                return true;
            }

            return false;
        }
    }
}