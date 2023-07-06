using Polly;
using NSE.WebApp.MVC.Interfaces;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using static NSE.WebApp.MVC.Extensions.CpfAttributeAdapter;
using NSE.WebApi.Core.User;
using NSE.WebApp.MVC.Services.Interfaces;
using NSE.WebApi.Core.Extensions;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            #region  HttpServices

            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>()
                .AllowSelfSignedCertificate()
                .AddPolicyHandler(PollyExtensions.WaitAndTry())
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            services
                .AddHttpClient<ICatalogoService, CatalogoService>()
                .AllowSelfSignedCertificate()
                .AddPolicyHandler(PollyExtensions.WaitAndTry())
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)))
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
           
            services
                .AddHttpClient<IComprasBffService, ComprasBffService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AllowSelfSignedCertificate()
                .AddPolicyHandler(PollyExtensions.WaitAndTry())
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
           
            services
                .AddHttpClient<IClienteService, ClienteService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AllowSelfSignedCertificate()
                .AddPolicyHandler(PollyExtensions.WaitAndTry())
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            return services;

            #endregion
        }
    }
}