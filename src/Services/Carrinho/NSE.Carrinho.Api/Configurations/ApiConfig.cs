using NSE.WebApi.Core.Identidade;

namespace NSE.Carrinho.Api.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthConfiguration(Configuration);
            services.AddCorsConfiguration();
            services.AddDbContextConfiguration();

            return services;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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