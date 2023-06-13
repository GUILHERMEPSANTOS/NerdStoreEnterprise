namespace Core.Messages.Integration
{
    public class OrderLoweredStockIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public OrderLoweredStockIntegrationEvent(Guid orderId, Guid customerId)
        {
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}