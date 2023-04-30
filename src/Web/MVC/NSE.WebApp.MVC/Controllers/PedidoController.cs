using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Controllers
{
    public class PedidoController : MainController
    {
        private readonly IClienteService _clienteService;
        private readonly IComprasBffService _comprasBffService;

        public PedidoController(IClienteService clienteService, IComprasBffService comprasBffService)
        {
            _clienteService = clienteService;
            _comprasBffService = comprasBffService;
        }

        [HttpGet]
        [Route("endereco-de-entrega")]
        public async Task<IActionResult> DeliveryAddress()
        {
            var shoppingCart = await _comprasBffService.GetShoppingCart();
            if (shoppingCart.Items.Count == 0) return RedirectToAction("Index", "Carrinho");

            var address = await _clienteService.GetAddress();
            var order = _comprasBffService.MapToOrder(shoppingCart, address);

            return View(order);
        }
    }
}