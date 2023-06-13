using NSE.MessageBus;
using NSE.Catalogo.API.Domain.Interfaces;
using NSE.Catalogo.API.Domain.Entities;
using Core.DomainObjects;
using Core.Messages.Integration;

namespace NSE.Catalogo.API.Services
{
    public class CatalogIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CatalogIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        public async Task SetSubscribers()
        {
            await _bus.SubscribeAsync<OrderAuthorizedIntegrationEvent>("OrderAuthorizedIntegrationEvent", WriteDownInventory);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await SetSubscribers();
        }

        // TODO - 12/06/2023 - Reduzir as responsabilidades
        private async Task WriteDownInventory(OrderAuthorizedIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();

            var productWithStock = new List<Product>();
            var productRepository = scope.ServiceProvider.GetService<IProductRepository>();
            var productsIds = message.Items.Select(item => item.Key);
            var products = await productRepository.GetProducts(productsIds);

            if (products.Count() != message.Items.Count())
            {
                await CancelOrderWithoutStock(message);
                return;
            }

            foreach (var product in products)
            {
                var productUnits = message.Items.FirstOrDefault(item => item.Key == product.Id).Value;

                var productIsAvailable = product.IsAvailable(productUnits);

                if (productIsAvailable)
                {
                    product.TakeFromInventory(productUnits);
                    productWithStock.Add(product);
                }
            }

            if (productWithStock.Count != message.Items.Count())
            {
                await CancelOrderWithoutStock(message);
                return;
            }

            foreach (var product in productWithStock)
            {
                productRepository.Update(product);
            }

            if (!await productRepository.UnitOfWork.Commit())
            {
                throw new DomainException($"Ocorreu um problema ao atualizar o estoque {message.OrderId}");
            }

            var productTaken = new OrderLoweredStockIntegrationEvent(message.OrderId, message.CustomerId);
            await _bus.PublishAsync(productTaken);
        }

        private async Task CancelOrderWithoutStock(OrderAuthorizedIntegrationEvent message)
        {
            var orderCancelled = new OrderCancelledIntegrationEvent(message.OrderId, message.CustomerId);
            await _bus.PublishAsync(orderCancelled);
        }
    }
}