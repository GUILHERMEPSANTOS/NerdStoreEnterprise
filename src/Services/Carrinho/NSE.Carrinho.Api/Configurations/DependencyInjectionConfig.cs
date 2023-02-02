using NSE.Carrinho.Api.Application.Interfaces;
using NSE.Carrinho.Api.Application.Services;
using NSE.Carrinho.Api.Data.Repositories;
using NSE.Carrinho.Api.Domain.Interfaces;
using NSE.WebApi.Core.User;

namespace NSE.Carrinho.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            return services;
        }
    }
}