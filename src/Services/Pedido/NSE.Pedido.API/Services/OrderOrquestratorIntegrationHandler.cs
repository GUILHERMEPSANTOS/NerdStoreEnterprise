using NSE.Pedido.API.Application.Queries;

namespace NSE.Pedido.API.Services
{
    public class OrderOrquestratorIntegrationHandler : IHostedService, IDisposable
    {
        private readonly ILogger<OrderOrquestratorIntegrationHandler> _logger;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;


        public OrderOrquestratorIntegrationHandler(IServiceProvider serviceProvider, ILogger<OrderOrquestratorIntegrationHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço de pedidos iniciado.");

            _timer = new Timer(OrquestrateOrders, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private async void OrquestrateOrders(object state)
        {
            _logger.LogInformation("Processando pedido");

            using var scope = _serviceProvider.CreateScope();
            var orderQueries = scope.ServiceProvider.GetService<IOrderQueries>();

            var orders = await orderQueries.GetAuthorizedOrders();

            _logger.LogDebug("Orders", orders);

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(Timeout.Infinite, 0);

            _logger.LogInformation("O Serviço de pedidos parou");

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}