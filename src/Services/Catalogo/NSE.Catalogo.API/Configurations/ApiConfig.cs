namespace NSE.Catalogo.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerConfiguration();
            services.AddCorsConfiguration();
            services.AddDbContextConfiguration(Configuration);

            return services;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerConfiguration();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCorsConfiguration();
            
            app.UseRouting();


            app.MapControllers();

            return app;
        }
    }
}