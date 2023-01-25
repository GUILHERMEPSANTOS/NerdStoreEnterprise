using Core.Mediator;
using Core.Messages.Integration;
using EasyNetQ;
using FluentValidation.Results;
using NSE.Cliente.API.Application.Customer.Commands;

namespace NSE.Cliente.API.Services
{
    public class NewCustomerIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public NewCustomerIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            await _bus.Rpc.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(AddCustomer);
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