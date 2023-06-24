using Microsoft.Extensions.Options;
using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Services.Grpc;
using NSE.Bff.Compras.Services.Grpc.Interfaces;
using static NSE.Carrinho.Api.Services.gRPC.ShoppingCartOrders;

namespace NSE.Bff.Compras.Configurations
{
    public static class GrpcConfig
    {
        public static IServiceCollection AddGrpcServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<GrpcServiceInterceptor>();

            services.AddScoped<IShoppingCartGrpcService, ShoppingCartGrpcService>();

            services.AddGrpcClient<ShoppingCartOrdersClient>((serivce, options) =>
            {
                var urlsConfig = serivce.GetRequiredService<IOptions<AppServicesSettings>>();
                options.Address = new Uri(urlsConfig.Value.CarrinhoUrl);
            }).AddInterceptor<GrpcServiceInterceptor>();

            return services;
        }
    }
}