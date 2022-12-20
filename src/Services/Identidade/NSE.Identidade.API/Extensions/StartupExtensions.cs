using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NSE.Identidade.API.Extensions
{
    public static class StartupExtensions
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder WebApplicationBuilder) where TStartup : IStartup
        {
            var startup = Activator.CreateInstance(typeof(TStartup), WebApplicationBuilder.Configuration) as IStartup;

            if (startup is null) throw new ArgumentException("Classe Startup inválida");

            startup.ConfigureServices(WebApplicationBuilder.Services);

            var app = WebApplicationBuilder.Build();

            startup.Configure(app, app.Environment);

            app.Run();

            return WebApplicationBuilder;
        }
    }
}