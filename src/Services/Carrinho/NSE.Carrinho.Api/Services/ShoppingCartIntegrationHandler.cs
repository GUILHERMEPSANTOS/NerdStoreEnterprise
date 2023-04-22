using Core.Messages.Integration;
using NSE.Carrinho.Api.Application.Interfaces;
using NSE.MessageBus;

namespace NSE.Carrinho.Api.Services
{
    public class ShoppingCartIntegrationHandler : BackgroundService
    {
        private IServiceProvider _serviceProvider;
        private IMessageBus _bus;

        public ShoppingCartIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private async Task SetSubscriber()
        {
            await _bus.SubscribeAsync<OrderDoneIntegrationEvent>("OrderDone", RemoveShoppingCart);
        }

        private async Task RemoveShoppingCart(OrderDoneIntegrationEvent @event)
        {
            using var scope = _serviceProvider.CreateScope();
            var serviceShoppingCart = scope.ServiceProvider.GetRequiredService<IShoppingCartService>();

            await serviceShoppingCart.DeleteShoppingCartBy(@event.CustomerId);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await SetSubscriber();
        }
    }
}