using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Extensions
{
    public class ShoppingCartViewComponent : ViewComponent
    {

        private readonly IComprasBffService _comprasBffService;

        public ShoppingCartViewComponent(IComprasBffService carrinhoService)
        {
            _comprasBffService = carrinhoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _comprasBffService.GetShoppingCartItemsQuantity());
        }
    }
}