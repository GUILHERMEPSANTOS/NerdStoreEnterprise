using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Identidade.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection builder)
        {
            builder.AddControllers();
            builder.AddEndpointsApiExplorer();

            return builder;
        }

        public static WebApplication UseApplicationConfiguration(this WebApplication app, IWebHostEnvironment environment)
        {

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseJWTConfiguration();

            app.MapControllers();

            return app;
        }
    }
}
