using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Interfaces;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalogoController : MainController
    {
        private readonly ICatalogoServiceRefit _catalogoServiceRefit;

        public CatalogoController(ICatalogoServiceRefit catalogoServiceRefit)
        {
            _catalogoServiceRefit = catalogoServiceRefit;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _catalogoServiceRefit.GetAll();

            return View(produtos);
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produto = await _catalogoServiceRefit.GetById(id);

            return View(produto);
        }
    }
}