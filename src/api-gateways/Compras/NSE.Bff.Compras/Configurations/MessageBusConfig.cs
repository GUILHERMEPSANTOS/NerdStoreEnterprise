using Core.Utils;
using NSE.MessageBus;

namespace NSE.Bff.Compras.Configurations;

public static class MessageBusConfig
{
    public static IServiceCollection AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));

        return services;
    }
}
