using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.WebApi.Core.Identidade;

namespace NSE.Identidade.API.Configurations
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();
         
            services.AddJwtConfiguration(Configuration);
         
            services.AddEndpointsApiExplorer();

            return services;
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
