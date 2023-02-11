using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Interfaces;
using NSE.WebApp.MVC.Models.Carrinho;
using NSE.WebApp.MVC.Models.Catalogo;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Controllers
{
    public class CarrinhoController : MainController
    {

        private readonly ICatalogoService _catalogoService;
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhoController(ICatalogoService catalogoService, ICarrinhoService carrinhoService)
        {
            _catalogoService = catalogoService;
            _carrinhoService = carrinhoService;
        }

        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            var result = await _carrinhoService.GetShoppingCart();

            return View(result);
        }

        [HttpPost]
        [Route("carrinho/adicionar-item")]
        public async Task<IActionResult> AddCartItem(CartItemViewModel item)
        {
            var product = await _catalogoService.GetById(item.ProductId);

            ValidateCartItem(product, item.Quantity);
            if (!IsValid()) return View("Index", await _carrinhoService.GetShoppingCart());

            item.Name = product.Name;
            item.Image = product.Image;
            item.Price = product.Price;

            var result = await _carrinhoService.AddCartItem(item);

            if (HasErrors(result)) return View("Index", await _carrinhoService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/atualizar-item")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, int quantity)
        {
            var product = await _catalogoService.GetById(productId);

            ValidateCartItem(product, quantity);

            if (!IsValid()) return View("Index", await _carrinhoService.GetShoppingCart());

            var item = new CartItemViewModel { ProductId = productId, Quantity = quantity };
            var result = await _carrinhoService.UpdateCartItem(productId, item);

            if (HasErrors(result)) return View("Index", await _carrinhoService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/remover-item")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var product = await _catalogoService.GetById(productId);

            if (product is null)
            {
                AddValidationError("Produto inválido");
                return View("Index", await _carrinhoService.GetShoppingCart());
            }

            var result = await _carrinhoService.RemoveCartItem(productId);

            return RedirectToAction("Index");
        }

        private void ValidateCartItem(ProdutoViewModel product, int quantity)
        {
            if (product is null) AddValidationError("Produto inexistente");
            if (quantity < 1) AddValidationError($"Escolha ao menos uma unidade do produto {product.Name}");
            if (quantity > product.StockQuantity) AddValidationError($"O Produto {product.Name} possui {product.StockQuantity} unidades em estoque, você selecionou {quantity}");
        }
    }
}