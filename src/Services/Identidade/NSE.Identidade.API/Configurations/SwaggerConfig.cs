using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Identidade.API.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection builder)
        {
            builder.AddSwaggerGen(
                c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Guilherme Pereira",
                        Email = "guilhermepereiradossantos@outlook.com"
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                })
            );

            return builder;
        }


        public static WebApplication UseSwaggerConfiguration(this WebApplication app, IWebHostEnvironment environment)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1")
                );
            }

            return app;
        }
    }
}