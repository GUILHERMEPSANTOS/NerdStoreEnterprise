using Core.Utils;
using NSE.Catalogo.API.Services;
using NSE.MessageBus;

namespace NSE.Catalogo.API.Configurations
{
    public static class MessageBusConfig
    {
        public static IServiceCollection AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CatalogIntegrationHandler>();

            return services;
        }
    }
}