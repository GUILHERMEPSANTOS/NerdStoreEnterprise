using Core.Utils;
using NSE.Carrinho.Api.Services;
using NSE.MessageBus;

namespace NSE.Carrinho.Api.Configurations
{
    public static class MessageBusConfig
    {
        public static IServiceCollection AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<ShoppingCartIntegrationHandler>();

            return services;
        }
    }
}
