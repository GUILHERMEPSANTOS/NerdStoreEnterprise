using AutoMapper;
using NSE.Cliente.API.Application.Customer.Commands;
using NSE.Cliente.API.Application.DTO;

namespace NSE.Cliente.API.Application.Mappings
{
    public class DTOToCommandMappingProfile : Profile
    {
        protected DTOToCommandMappingProfile()
        {
            CreateMap<NewCustomerDTO, NewCustomerCommand>();
        }
    }
}