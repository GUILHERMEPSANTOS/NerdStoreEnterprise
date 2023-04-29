
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Application.Customer.Queries;
using NSE.Cliente.API.Application.DTO;
using NSE.Cliente.API.Application.Interfaces;
using NSE.WebApi.Core.Controllers;

namespace NSE.Cliente.API.Controllers
{
    [Authorize, Route("customers")]
    public class CustomerController : MainController
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerAddressQuery _customerAddressQuery;

        public CustomerController(ICustomerService customerService, ICustomerAddressQuery customerAddressQuery)
        {
            _customerService = customerService;
            _customerAddressQuery = customerAddressQuery;
        }

        [HttpPost("address")]
        public async Task<IActionResult> AddAddress([FromBody] AddressDTO address)
        {
            var result = await _customerService.AddAddress(address);

            return CustomResponse(result);
        }

        [HttpGet("address")]
        public async Task<IActionResult> GetAddress()
        {
            var address = await _customerAddressQuery.GetAddress();

            return address is null ? NotFound() : CustomResponse(address);
        }

    }
}