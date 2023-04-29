
using NSE.Cliente.API.Application.DTO;

namespace NSE.Cliente.API.Application.Customer.Queries
{
    public interface ICustomerAddressQuery
    {
        Task<AddressDTO> GetAddress();
    }
}