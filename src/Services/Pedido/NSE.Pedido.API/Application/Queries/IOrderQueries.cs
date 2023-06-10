

using NSE.Pedido.API.Application.DTO;

namespace NSE.Pedido.API.Application.Queries
{
    public interface IOrderQueries
    {
        Task<OrderDTO> GetLastOrder(Guid customerId);
        Task<IEnumerable<OrderDTO>> GetOrdersBy(Guid customerId);
        Task<OrderDTO> GetAuthorizedOrders();
    }
}