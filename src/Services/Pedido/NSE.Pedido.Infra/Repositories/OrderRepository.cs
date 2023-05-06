using System.Data.Common;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using NSE.Pedido.Domain.Orders;
using NSE.Pedido.Domain.Orders.Interfaces;
using NSE.Pedido.Infra.Context;

namespace NSE.Pedido.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersContext _orderContext;
        public IUnitOfWork UnitOfWork => _orderContext;

        public OrderRepository(OrdersContext orderContext)
        {
            _orderContext = orderContext;
        }

        public void Add(Order order)
        {
            _orderContext.Add(order);
        }

        public async Task<Order> GetBy(Guid id)
        {
            return await _orderContext.Orders.FindAsync(id);
        }

        public async Task<OrderItem> GetOrderItemBy(Guid id)
        {
            return await _orderContext.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetOrderItemBy(Guid orderId, Guid productId)
        {
            return await _orderContext.OrderItems
                .FirstOrDefaultAsync(orderItem => orderItem.OrderId == orderId && orderItem.ProductId == productId);
        }

        public async Task<IEnumerable<Order>> GetOrdersBy(Guid customerId)
        {
            return await _orderContext.Orders
                .AsNoTracking()
                .Include(order => order.OrderItems)
                .Where(order => order.CustomerId == customerId)
                .ToArrayAsync();
        }

        public void Update(Order order)
        {
            _orderContext.Update(order);
        }

        public void Dispose()
        {
            _orderContext.Dispose();
        }

        public DbConnection GetDbConnection()
        {
            return _orderContext.Database.GetDbConnection();
        }
    }
}