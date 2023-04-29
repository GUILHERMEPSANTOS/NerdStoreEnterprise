using AutoMapper;
using NSE.Cliente.API.Application.Customer.Commands;
using NSE.Cliente.API.Application.DTO;

namespace NSE.Cliente.API.Application.Mappings
{
    public class DTOToCommandMappingProfile : Profile
    {
        public DTOToCommandMappingProfile()
        {
            CreateMap<NewCustomerDTO, NewCustomerCommand>();
            CreateMap<AddressDTO, AddAddressCommand>();
        }
    }
}