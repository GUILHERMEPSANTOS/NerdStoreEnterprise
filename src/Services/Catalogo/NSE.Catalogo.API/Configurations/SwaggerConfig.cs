using Microsoft.OpenApi.Models;

namespace NSE.Catalogo.API.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NerdStore Enterprise Catálogo App",
                    Description = "Asp.Net Core Enterprise",
                    Contact = new OpenApiContact
                    {
                        Email = "guilhermepereiradossantos41@outlook.com",
                        Name = "Guilherme Pereira dos Santos"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            });

            return services;
        }

        public static WebApplication UseSwaggerConfiguration(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                  c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1")
              );

            return app;
        }
    }
}