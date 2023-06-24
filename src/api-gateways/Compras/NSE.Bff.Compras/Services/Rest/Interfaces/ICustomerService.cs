

using NSE.Bff.Compras.DTOs;

namespace NSE.Bff.Compras.Services.Interfaces
{
    public interface ICustomerService
    {
          Task<AddressDTO> GetAddress();
    }
}