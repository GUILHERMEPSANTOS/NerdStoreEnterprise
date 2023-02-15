using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services.Interfaces;

namespace NSE.WebApp.MVC.Extensions
{
    public class ShoppingCartViewComponent : ViewComponent
    {

        private readonly ICarrinhoService _carrinhoService;

        public ShoppingCartViewComponent(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _carrinhoService.GetShoppingCart());
        }
    }
}