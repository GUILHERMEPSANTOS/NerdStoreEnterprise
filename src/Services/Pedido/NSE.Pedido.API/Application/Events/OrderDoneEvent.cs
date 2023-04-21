using Core.Messages;

namespace NSE.Pedido.API.Application.Events
{
    public class OrderDoneEvent : Event
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }

        public OrderDoneEvent(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}