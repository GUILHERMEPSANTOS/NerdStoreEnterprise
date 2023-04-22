
using Core.Mediator;
using MediatR;
using NSE.Cliente.API.Configuration;
using NSE.Pedido.API.Application.Queries;
using NSE.Pedido.Domain.Orders.Interfaces;
using NSE.Pedido.Domain.Vouchers;
using NSE.Pedido.Infra.Repositories;
using NSE.WebApi.Core.User;

namespace NSE.Pedido.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            
            services.AddScoped<IVoucherRepository, VoucherRepository>();
            services.AddScoped<IVoucherQueries, VoucherQueries>();

            services.AddMessageBusConfig(configuration);

            return services;
        }
    }
}