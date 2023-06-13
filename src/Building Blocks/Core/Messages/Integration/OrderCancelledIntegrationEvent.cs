namespace Core.Messages.Integration
{
    public class OrderCancelledIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public OrderCancelledIntegrationEvent(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}