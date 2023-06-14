using Core.DomainObjects;
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
        private async Task SetSubscribersAsync()
        {
            await _bus.SubscribeAsync<OrderCancelledIntegrationEvent>("OrderCanceledIntegrationEvent", CancelTransaction);

            await _bus.SubscribeAsync<OrderLoweredStockIntegrationEvent>("OrderLoweredStockIntegrationEvent", CapturePayment);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await SetSubscribersAsync();
            await SetResponder();
        }
        private async Task CapturePayment(OrderLoweredStockIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var billingService = scope.ServiceProvider.GetService<IBillingService>();

            var response = await billingService.GetTransaction(message.OrderId);

            if (!response.ValidationResult.IsValid)
                throw new DomainException($"Falha ao capturar o pagamento {message.OrderId}");

            await _bus.PublishAsync(new OrderPaidIntegrationEvent(message.CustomerId, message.OrderId));
        }

        private async Task CancelTransaction(OrderCancelledIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();

            var pagamentoService = scope.ServiceProvider.GetRequiredService<IBillingService>();

            var Response = await pagamentoService.CancelTransaction(message.OrderId);

            if (!Response.ValidationResult.IsValid)
                throw new DomainException($"Failed to cancel order payment {message.OrderId}");
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