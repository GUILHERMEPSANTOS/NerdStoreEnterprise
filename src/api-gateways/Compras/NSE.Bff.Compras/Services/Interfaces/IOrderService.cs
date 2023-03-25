using NSE.Bff.Compras.DTOs;

namespace NSE.Bff.Compras.Services.Interfaces
{
    public interface IOrderService
    {
        Task<VoucherDTO> GetVoucherByCode(string code);
    }
}