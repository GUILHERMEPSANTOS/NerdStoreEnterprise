using Core.Utils;
using NSE.MessageBus;
using NSE.Pedido.API.Services;

namespace NSE.Cliente.API.Configuration;
public static class MessageBusConfig
{
    public static IServiceCollection AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<OrderOrquestratorIntegrationHandler>(); ;

        return services;
    }
}
