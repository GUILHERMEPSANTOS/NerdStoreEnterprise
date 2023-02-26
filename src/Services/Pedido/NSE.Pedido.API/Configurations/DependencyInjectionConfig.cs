
using Core.Mediator;
using MediatR;

namespace NSE.Pedido.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            return services;
        }
    }
}