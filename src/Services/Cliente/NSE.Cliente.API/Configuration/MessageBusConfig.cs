using Core.Utils;
using NSE.Cliente.API.Services;
using NSE.MessageBus;

namespace NSE.Cliente.API.Configuration;
public static class MessageBusConfig
{
    public static IServiceCollection AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<NewCustomerIntegrationHandler>();

        return services;
    }
}
