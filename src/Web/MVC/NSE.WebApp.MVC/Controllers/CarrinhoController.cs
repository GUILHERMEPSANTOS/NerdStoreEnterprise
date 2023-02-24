using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models.Carrinho;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Controllers
{
    public class CarrinhoController : MainController
    {
        private readonly IComprasBffService _comprasBffService;

        public CarrinhoController(IComprasBffService comprasBffService)
        {
            _comprasBffService = comprasBffService;
        }

        [HttpGet]
        [Route("carrinho")]
        public async Task<IActionResult> Index()
        {
            var result = await _comprasBffService.GetShoppingCart();

            return View(result);
        }

        [HttpPost]
        [Route("carrinho/adicionar-item")]
        public async Task<IActionResult> AddCartItem(CartItemViewModel item)
        {
            ModelState.Clear();

            var result = await _comprasBffService.AddCartItem(item);

            if (HasErrors(result)) return View("Index", await _comprasBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/atualizar-item")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, int quantity)
        {
            ModelState.Clear();

            var item = new CartItemViewModel { ProductId = productId, Quantity = quantity };
            var result = await _comprasBffService.UpdateCartItem(productId, item);

            if (HasErrors(result)) return View("Index", await _comprasBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("carrinho/remover-item")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var result = await _comprasBffService.RemoveCartItem(productId);

            if (HasErrors(result)) return View("Index", await _comprasBffService.GetShoppingCart());

            return RedirectToAction("Index");
        }
    }
}