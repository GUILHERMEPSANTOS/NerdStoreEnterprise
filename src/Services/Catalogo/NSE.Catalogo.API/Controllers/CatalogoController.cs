using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalogo.API.Application.DTOs;
using NSE.Catalogo.API.Application.Services.Interfaces;
using NSE.WebApi.Core.Controllers;
using NSE.WebApi.Core.Identidade.Filter;

namespace NSE.Catalogo.API.Controllers
{
    [Authorize]
    [Route("catalogo")]
    public class CatalogoController : MainController
    {
        private readonly IProductService _produtoService;

        public CatalogoController(IProductService produtoService)
        {
            _produtoService = produtoService;
        }

        [AllowAnonymous]
        [HttpGet("produtos")]
        public async Task<IActionResult> GetPagedProducts([FromQuery] RequestPagedProducts request)
        {
            var result = await _produtoService.GetPagedProducts(request.PageSize, request.PageIndex, request.Query);

            return CustomResponse(result);
        }

        [ClaimsAuthorize("Catalogo", "Read")]
        [HttpGet("produto/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _produtoService.GetById(id);

            return CustomResponse(result);
        }


        [HttpGet("produtos/{ids}")]
        public async Task<IActionResult> GetProducts([FromRoute] string ids)
        {
            var result = await _produtoService.GetProducts(ids);

            return CustomResponse(result);
        }
    }
}