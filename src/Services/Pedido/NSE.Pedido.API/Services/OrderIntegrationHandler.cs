using Core.DomainObjects;
using Core.Messages.Integration;
using NSE.MessageBus;
using NSE.Pedido.Domain.Orders.Interfaces;

namespace NSE.Pedido.API.Services
{
    public class OrderIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public OrderIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bus.SubscribeAsync<OrderCancelledIntegrationEvent>("OrderCancelledIntegrationEvent", CancelOrder);

            await _bus.SubscribeAsync<OrderPaidIntegrationEvent>("OrderPaidIntegrationEvent", FinishOrder);
        }

        private async Task CancelOrder(OrderCancelledIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();

            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

            var order = await orderRepository.GetBy(message.OrderId);
            order.Cancel();

            orderRepository.Update(order);

            if (!await orderRepository.UnitOfWork.Commit())
            {
                throw new DomainException($"Ocrreu um problema ao cancelar o pedido: {message.OrderId}");
            }
        }

        private async Task FinishOrder(OrderPaidIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();

            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

            var order = await orderRepository.GetBy(message.OrderId);
            order.Finish();

            orderRepository.Update(order);

            if (!await orderRepository.UnitOfWork.Commit())
            {
                throw new DomainException($"Problemas ao finalizar pedido: {message.OrderId}");
            }
        }
    }
}