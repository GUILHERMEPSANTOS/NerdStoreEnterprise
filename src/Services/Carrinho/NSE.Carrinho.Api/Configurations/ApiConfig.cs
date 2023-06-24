using NSE.Carrinho.API.Services.gRPC;
using NSE.WebApi.Core.Identidade;

namespace NSE.Carrinho.Api.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();
            services.AddGrpc();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAuthConfiguration(Configuration);
            services.AddCorsConfiguration();
            services.AddDbContextConfiguration(Configuration);
            services.AddDependencyInjectionConfiguration(Configuration);
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
            app.MapGrpcService<ShoppingCartGrpcService>()
               .RequireCors("All");
                
            return app;
        }
    }
}