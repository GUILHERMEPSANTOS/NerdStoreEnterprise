using NSE.Pagamento.API.Data.Repositories;
using NSE.Pagamento.API.Domain;
using NSE.Pagamento.API.Facade;
using NSE.Pagamento.API.Services;
using NSE.WebApi.Core.User;

namespace NSE.Pagamento.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.Configure<BillingConfig>(configuration.GetSection("BillingConfig"));

            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IPaymentFacade, CreditCardPaymentFacade>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();

            services.AddMessageBusConfig(configuration);
            return services;
        }
    }
}