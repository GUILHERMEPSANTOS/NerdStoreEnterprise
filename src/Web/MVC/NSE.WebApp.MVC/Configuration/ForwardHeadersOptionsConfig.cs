using Microsoft.AspNetCore.HttpOverrides;

namespace NSE.WebApp.MVC.Configuration
{
    public static class ForwardHeadersOptionsConfig
    {
        public static IServiceCollection AddForwardHeadersConfiguration(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            return services;
        }
    }
}