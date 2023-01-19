namespace NSE.Cliente.API.Configuration
{
    public static class StartupConfig
    {
        public static WebApplicationBuilder UseStartup<TStartup>(this WebApplicationBuilder WebApplicationBuilder) where TStartup : IStartup
        {

            var startup = Activator.CreateInstance(typeof(TStartup), WebApplicationBuilder.Environment) as IStartup;

            if (startup is null)
            {
                throw new ArgumentNullException("startup inválida");
            }

            startup.ConfigureServices(WebApplicationBuilder.Services);

            var app = WebApplicationBuilder.Build();

            startup.Configure(app, app.Environment);

            app.Run();

            return WebApplicationBuilder;
        }
    }
}