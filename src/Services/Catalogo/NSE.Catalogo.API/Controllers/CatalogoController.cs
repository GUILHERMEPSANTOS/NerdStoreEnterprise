using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Application.Services.Interfaces;
using NSE.Catalogo.API.Domain.Entities;

namespace NSE.Catalogo.API.Controllers
{
    [ApiController]
    [Route("catalogo")]
    public class CatalogoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public CatalogoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("produtos")]
        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _produtoService.GetAll();
        }

        [HttpGet("produto/{id}")]
        public async Task<Produto> GetById([FromRoute] Guid id)
        {
            return await _produtoService.GetById(id);
        }
    }
}