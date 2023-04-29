using AutoMapper;
using NSE.Cliente.API.Application.DTO;
using NSE.Cliente.API.Domain.Entities;

namespace NSE.Cliente.API.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Address, AddressDTO>();
        }
    }
}