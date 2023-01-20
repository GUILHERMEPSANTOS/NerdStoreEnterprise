using Core.Mediator;
using MediatR;
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

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<ICustomerService, CustomerService>();

            return services;
        }
    }
}