using AutoMapper;
using NSE.Cliente.API.Application.DTO;
using NSE.Cliente.API.Domain.Entities;
using NSE.Cliente.API.Domain.Interfaces;
using NSE.WebApi.Core.User;

namespace NSE.Cliente.API.Application.Customer.Queries
{
    public class CustomerAddressQuery : ICustomerAddressQuery
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAspNetUser _user;
        private readonly IMapper _mapper;

        public CustomerAddressQuery(ICustomerRepository customerRepository, IAspNetUser user, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _user = user;
            _mapper = mapper;
        }

        public async Task<AddressDTO> GetAddress()
        {
            var customerId = _user.GetUserId();
            var address = await _customerRepository.GetAddressBy(customerId);
            
            return _mapper.Map<AddressDTO>(address);
        }
    }
}