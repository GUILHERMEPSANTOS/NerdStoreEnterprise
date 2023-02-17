using NSE.WebApi.Core.User;

namespace NSE.Bff.Compras.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, IHttpContextAccessor>();

            return services;
        }
    }
}