namespace Core.Messages.Integration
{
    public class OrderDoneIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; private set; }

        public OrderDoneIntegrationEvent(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}