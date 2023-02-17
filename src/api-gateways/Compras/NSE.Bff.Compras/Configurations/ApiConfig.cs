using NSE.Bff.Compras.Extensions;
using NSE.WebApi.Core.Identidade;

namespace NSE.Bff.Compras.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<AppServicesSettings>(configuration);

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors();
            services.AddMessageBusConfig(configuration);
            services.AddAuthConfiguration(configuration);
            services.AddSwaggerConfiguration();

            return services;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerConfiguration();
            }

            app.UseHttpsRedirection();

            app.UseCorsConfiguration();

            app.UseRouting();

            app.UseAuthConfiguration();

            app.MapControllers();

            return app;
        }
    }
}