using MediatR;
using Core.Mediator;
using NSE.Cliente.API.Application.Interfaces;
using NSE.Cliente.API.Application.Mappings;
using NSE.Cliente.API.Application.Service;
using NSE.Cliente.API.Data.Repositories;
using NSE.Cliente.API.Domain.Interfaces;
using NSE.WebApi.Core.User;
using NSE.Cliente.API.Application.Customer.Queries;

namespace NSE.Cliente.API.Configuration
{
    public static class DependencyInjectionCongig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Http Accessor
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(DTOToCommandMappingProfile));
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            #endregion

            #region Mediator
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            #endregion

            #region Services/Repositories 
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            #endregion

            #region Queries 
            services.AddScoped<ICustomerAddressQuery, CustomerAddressQuery>();
            #endregion
            
            return services;
        }
    }
}