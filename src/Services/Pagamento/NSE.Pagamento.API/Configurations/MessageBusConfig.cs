using Core.Utils;
using NSE.MessageBus;
using NSE.Pagamento.API.Services;

namespace NSE.Pagamento.API.Configurations;
public static class MessageBusConfig
{
    public static IServiceCollection AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<BillingIntegrationHandler>(); ;

        return services;
    }
}
