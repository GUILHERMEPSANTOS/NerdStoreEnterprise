using AutoMapper;
using Core.Mediator;
using FluentValidation.Results;
using NSE.Cliente.API.Application.Customer.Commands;
using NSE.Cliente.API.Application.DTO;
using NSE.Cliente.API.Application.Interfaces;

namespace NSE.Cliente.API.Application.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public CustomerService(IMapper mapper, IMediatorHandler mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<ValidationResult> Add(NewCustomerDTO newCustomerDTO)
        {
            var newCustomerCommand = _mapper.Map<NewCustomerCommand>(newCustomerDTO);

            return await _mediator.SendCommand<NewCustomerCommand>(newCustomerCommand);
        }
    }
}