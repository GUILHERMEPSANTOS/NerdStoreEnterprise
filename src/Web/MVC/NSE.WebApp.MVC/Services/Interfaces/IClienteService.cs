using Core.Communication;
using NSE.WebApp.MVC.Models.Cliente;

namespace NSE.WebApp.MVC.Services.Interfaces
{
    public interface IClienteService
    {
        Task<ResponseResult> AddAddress(AddressViewModel address);
        Task<AddressViewModel> GetAddress();
    }
}