using Core.DomainObjects.Data;

namespace NSE.Pedido.Domain.Orders.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetBy(Guid id);
        Task<IEnumerable<Order>> GetOrdersBy(Guid customerId);
        void Add(Order order);
        void Update(Order order);
        Task<OrderItem> GetOrderItemBy(Guid id);
        Task<OrderItem> GetOrderItemBy(Guid orderId, Guid productId);
    }
}