using NSE.WebApi.Core.Identidade;

namespace NSE.Cliente.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();
            services.AddCorsConfiguration();
            services.AddDbContextConfiguration(Configuration);
            services.AddEndpointsApiExplorer();
            services.AddJwtConfiguration(Configuration);
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

            app.UseJWTConfiguration();

            app.MapControllers();

            return app;
        }
    }
}