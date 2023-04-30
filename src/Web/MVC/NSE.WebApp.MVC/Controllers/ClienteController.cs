
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models.Cliente;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Controllers
{
    public class ClienteController : MainController
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressViewModel address)
        {
            var response = await _clienteService.AddAddress(address);
            var hasErros   = HasErrors(response);

            if(hasErros) TempData["Errors"] = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();

            return RedirectToAction("DeliveryAddress", "Pedido");
        }
    }
}