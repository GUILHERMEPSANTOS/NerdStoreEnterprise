namespace Core.Messages.Integration
{
    public class OrderAuthorizedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public IDictionary<Guid, int> Items { get; private set; }

        public OrderAuthorizedIntegrationEvent(Guid orderId, Guid customerId, IDictionary<Guid, int> items)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Items = items;
        }
    }
}