using Core.Communication;
using NSE.Bff.Compras.DTOs;

namespace NSE.Bff.Compras.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseResult> FinishOrder(OrderDTO order);
        Task<OrderDTO> GetLastOrder();
        Task<IEnumerable<OrderDTO>> GetCustomers();
        Task<VoucherDTO> GetVoucherByCode(string code);
    }
}