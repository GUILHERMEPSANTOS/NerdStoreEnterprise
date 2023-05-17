namespace Core.Messages.Integration
{
    public class OrderInitiatedIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public int PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string CardNumber { get; set; }
        public string Holder { get; set; }
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
    }
}