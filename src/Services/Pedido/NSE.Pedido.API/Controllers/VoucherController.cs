using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.API.Application.Queries;
using NSE.WebApi.Core.Controllers;

namespace NSE.Pedido.API.Controllers
{
    [Authorize]
    [Route("voucher")]
    public class VoucherController : MainController
    {
        private readonly IVoucherQueries _voucherQueries;

        public VoucherController(IVoucherQueries voucherQueries)
        {
            _voucherQueries = voucherQueries;
        }

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(VoucherDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetVoucherByCode(string code)
        {
            if (string.IsNullOrEmpty(code)) return NotFound();

            var voucherDTO = await _voucherQueries.GetVoucherByCode(code);

            if (voucherDTO is null) return NotFound();

            return CustomResponse(voucherDTO);
        }
    }
}