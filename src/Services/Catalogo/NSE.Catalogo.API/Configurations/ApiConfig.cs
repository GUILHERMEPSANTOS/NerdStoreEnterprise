using NSE.WebApi.Core.Identidade;

namespace NSE.Catalogo.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddAuthConfiguration(Configuration);

            services.AddSwaggerConfiguration();

            services.AddCorsConfiguration();

            services.AddDbContextConfiguration(Configuration);

            services.AddDependencyInjectionConfiguration();

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