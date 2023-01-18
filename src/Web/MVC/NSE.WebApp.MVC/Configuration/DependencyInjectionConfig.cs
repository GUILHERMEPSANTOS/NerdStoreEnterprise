using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Interfaces;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>();

            // services.AddHttpClient<ICatalogoService, CatalogoService>()
            //     .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


            services.AddHttpClient("Refit",
                     options =>
                    {
                        options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
                    })
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();

            return services;
        }
    }
}