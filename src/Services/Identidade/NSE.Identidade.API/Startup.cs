using NSE.Identidade.API.Configurations;

namespace NSE.Identidade.API
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
            services.AddApiConfiguration();

            services.AddIdentityConfiguration(Configuration);

            services.AddSwaggerConfiguration();

            services.AddJwtConfiguration(Configuration);
        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {
            app.UseSwaggerConfiguration(environment);

            app.UseApplicationConfiguration(environment);
        }
    }
}

