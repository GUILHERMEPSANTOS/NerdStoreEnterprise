
using Microsoft.AspNetCore.Mvc;
using NSE.Cliente.API.Application.DTO;
using NSE.Cliente.API.Application.Interfaces;
using NSE.WebApi.Core.Controllers;

namespace NSE.Cliente.API.Controllers
{
    [Route("customers")]
    public class CustomerController : MainController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {

            var newCustomerDTO = new NewCustomerDTO();

            newCustomerDTO.Cpf = "53225780057";

            newCustomerDTO.Email = "thiagopereiradossantos41@outlook.com";

            newCustomerDTO.Id = Guid.NewGuid();

            newCustomerDTO.Name = "Thiago";

            var result = await _customerService.Add(newCustomerDTO);

            return CustomResponse(result);
        }

    }
}