using Core.Mediator;
using Core.Messages.Integration;
using FluentValidation.Results;
using NSE.Cliente.API.Application.Customer.Commands;
using NSE.MessageBus;

namespace NSE.Cliente.API.Services
{
    public class NewCustomerIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public NewCustomerIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(AddCustomer);

            _bus.AdvancedBus.Connected += OnConnect;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
            }
        }

        private void OnConnect(object? sender, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> AddCustomer(UserRegisteredIntegrationEvent message)
        {
            var customerCommand = new NewCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult validationResult;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();

                validationResult = await mediator.SendCommand<NewCustomerCommand>(customerCommand);
            }

            return new ResponseMessage(validationResult);
        }
    }
}