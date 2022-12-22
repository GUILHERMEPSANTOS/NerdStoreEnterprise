using System.Reflection;

namespace NSE.WebApp.MVC.Configuration
{
    public static class StartupConfiguration
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder WebApplicationBuilder) where TStartup : IStartup
        {

            var startup = Activator.CreateInstance(typeof(TStartup), WebApplicationBuilder.Environment) as IStartup;

            if (startup is null) throw new ArgumentException("Classe Startup inválida");

            startup.ConfigureServices(WebApplicationBuilder.Services);
            
            
            var app = WebApplicationBuilder.Build();


            startup.Configure(app, app.Environment);

            app.Run();

            return WebApplicationBuilder;
        }
    }
}