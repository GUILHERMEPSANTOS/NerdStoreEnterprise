using NSE.WebApp.MVC.Configuration;

namespace NSE.WebApp.MVC
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment webHostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(webHostEnvironment.ContentRootPath)
                 .AddJsonFile("appsettings.json", true, true)
                 .AddJsonFile($"appsettings.{webHostEnvironment.EnvironmentName}.json", true, true)
                 .AddEnvironmentVariables();

            if (webHostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration();
            services.AddMvcCongiguration(Configuration);
            services.AddDependencyInjection();
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            app.UseMvcConfiguration();
        }
    }
}