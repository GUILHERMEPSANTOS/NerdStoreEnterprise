using NSE.Bff.Compras.Extensions;
using NSE.Bff.Compras.Services;
using NSE.Bff.Compras.Services.Grpc;
using NSE.Bff.Compras.Services.Grpc.Interfaces;
using NSE.Bff.Compras.Services.Interfaces;
using NSE.WebApi.Core.Extensions;
using NSE.WebApi.Core.User;
using Polly;

namespace NSE.Bff.Compras.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            #region HttpClient
            services.AddHttpClient<ICatalogService, CatalogService>()
                .AllowSelfSignedCertificate()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitAndTry())
                .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IShoppingCartService, ShoppingCartService>()
                .AllowSelfSignedCertificate()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitAndTry())
                .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<IOrderService, OrderService>()
                .AllowSelfSignedCertificate()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitAndTry())
                .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddHttpClient<ICustomerService, CustomerService>()
                .AllowSelfSignedCertificate()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddPolicyHandler(PollyExtensions.WaitAndTry())
                .AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
            #endregion
            return services;
        }
    }
}