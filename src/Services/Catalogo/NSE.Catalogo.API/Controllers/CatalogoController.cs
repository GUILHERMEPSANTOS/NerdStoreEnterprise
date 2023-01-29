using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Application.Services.Interfaces;
using NSE.Catalogo.API.Domain.Entities;
using NSE.WebApi.Core.Controllers;
using NSE.WebApi.Core.Identidade.Filter;

namespace NSE.Catalogo.API.Controllers
{
    [Authorize]
    [Route("catalogo")]
    public class CatalogoController : MainController
    {
        private readonly IProdutoService _produtoService;

        public CatalogoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [AllowAnonymous]
        [HttpGet("produtos")]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _produtoService.GetAll();
        }

        [ClaimsAuthorize("Catalogo", "Read")]
        [HttpGet("produto/{id}")]
        public async Task<Product> GetById([FromRoute] Guid id)
        {
            return await _produtoService.GetById(id);
        }
    }
}