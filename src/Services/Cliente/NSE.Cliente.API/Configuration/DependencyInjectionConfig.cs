using NSE.Cliente.API.Application.Interfaces;
using NSE.Cliente.API.Application.Mappings;
using NSE.Cliente.API.Application.Service;

namespace NSE.Cliente.API.Configuration
{
    public static class DependencyInjectionCongig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DTOToCommandMappingProfile));

            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}