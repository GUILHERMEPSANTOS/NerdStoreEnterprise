using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Interfaces;

namespace NSE.WebApp.MVC.Controllers
{
    public class CatalogoController : MainController
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 4, [FromQuery] string query = null)
        {
            var produtos = await _catalogoService.GetProductsWithPagination(pageIndex, pageSize, query);

            ViewBag.Search = query;
            produtos.ReferenceAction = "Index";

            return View(produtos);
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produto = await _catalogoService.GetById(id);

            return View(produto);
        }
    }
}