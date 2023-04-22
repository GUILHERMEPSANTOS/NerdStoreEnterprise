using NSE.WebApi.Core.Identidade;

namespace NSE.Pedido.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerConfiguration();
            services.AddDependencyInjection(configuration);
            services.AddAuthConfiguration(configuration);
            services.AddDbContextConfiguration(configuration);

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