using Core.Messages.Integration;
using NSE.MessageBus;
using NSE.Pagamento.API.Domain;

namespace NSE.Pagamento.API.Services
{
    public class BillingIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public BillingIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        private async Task SetResponder()
        {
            await _bus.RespondAsync<OrderInitiatedIntegrationEvent, ResponseMessage>(AuthorizePayment);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await SetResponder();
        }

        private async Task<ResponseMessage> AuthorizePayment(OrderInitiatedIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var billingService = scope.ServiceProvider.GetService<IBillingService>();

            var transaction = ToPayment(message);

            var responseMessage = await billingService.AuthorizePayment(transaction);

            return responseMessage;
        }

        public Payment ToPayment(OrderInitiatedIntegrationEvent message)
        {
            return new Payment
            {
                OrderId = message.OrderId,
                PaymentType = (PaymentType)message.PaymentType,
                Amount = message.Amount,
                CreditCard = new CreditCard(
                    message.Holder, message.CardNumber, message.ExpirationDate, message.SecurityCode)
            };
        }
    }
}