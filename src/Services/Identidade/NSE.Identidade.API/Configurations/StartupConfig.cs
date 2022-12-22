using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NSE.Identidade.API.Configurations
{
    public static class StartupConfig
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder WebApplicationBuilder) where TStartup : IStartup
        {
            var startup = Activator.CreateInstance(typeof(TStartup), WebApplicationBuilder.Environment) as IStartup;

            if (startup is null) throw new ArgumentException("Classe Startup inválida");

            var app = WebApplicationBuilder.Build();
            
            startup.ConfigureServices(WebApplicationBuilder.Services);

            startup.Configure(app, app.Environment);

            app.Run();

            return WebApplicationBuilder;
        }
    }
}