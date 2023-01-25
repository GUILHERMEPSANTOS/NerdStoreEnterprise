using NSE.Cliente.API.Configuration;
using NSE.WebApi.Core.Identidade;

namespace NSE.Identidade.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();

            services.AddIdentityConfiguration(Configuration);

            services.AddJwtConfiguration(Configuration);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerConfiguration();

            services.AddMessageBusConfig(Configuration);

            return services;
        }

        public static WebApplication UseApplicationConfiguration(this WebApplication app, IWebHostEnvironment environment)
        {
            app.UseSwaggerConfiguration(environment);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseJWTConfiguration();

            app.MapControllers();

            return app;
        }
    }
}
