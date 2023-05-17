namespace NSE.Pagamento.API.Configurations
{
    public static class CorsConfig
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("All", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            return services;
        }

        public static WebApplication UseCorsConfiguration(this WebApplication app)
        {
            app.UseCors("All");

            return app;
        }
    }
}