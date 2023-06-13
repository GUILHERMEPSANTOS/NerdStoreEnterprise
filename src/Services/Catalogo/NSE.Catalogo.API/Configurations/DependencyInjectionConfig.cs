using NSE.Catalogo.API.Application.Services;
using NSE.Catalogo.API.Application.Services.Interfaces;
using NSE.Catalogo.API.Data;
using NSE.Catalogo.API.Data.Repository;
using NSE.Catalogo.API.Domain.Interfaces;

namespace NSE.Catalogo.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddMessageBusConfig(configuration);

            return services;
        }
    }
}