using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models.Pedido;
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

        [HttpGet]
        [Route("pagamento")]
        public async Task<IActionResult> Payment()
        {
            var shoppingCart = await _comprasBffService.GetShoppingCart();
            if (shoppingCart.Items.Count == 0) return RedirectToAction("Index", "Carrinho");

            var order = _comprasBffService.MapToOrder(shoppingCart, null);

            return View(order);
        }

        [HttpPost]
        [Route("finalizar-pedido")]
        public async Task<IActionResult> FinishOrder(TransactionViewModel transaction)
        {   
            if (!ModelState.IsValid) return View("Payment", _comprasBffService.MapToOrder(await _comprasBffService.GetShoppingCart(), null));

            var retorno = await _comprasBffService.FinishOrder(transaction);

            if (HasErrors(retorno))
            {
                var shoppingCart = await _comprasBffService.GetShoppingCart();
                if (shoppingCart.Items.Count == 0) return RedirectToAction("Index", "Carrinho");

                var orderMap = _comprasBffService.MapToOrder(shoppingCart, null);
                return View("Payment", orderMap);
            }

            return RedirectToAction("OrderDone");
        }

        [HttpGet]
        [Route("pedido-concluido")]
        public async Task<IActionResult> OrderDone()
        {
            return View("OrderDone", await _comprasBffService.GetLastOrder());
        }

        [HttpGet("meus-pedidos")]
        public async Task<IActionResult> MyOrders()
        {
            var model = await _comprasBffService.GetCustomersById();
            return View(model);
        }
    }
}