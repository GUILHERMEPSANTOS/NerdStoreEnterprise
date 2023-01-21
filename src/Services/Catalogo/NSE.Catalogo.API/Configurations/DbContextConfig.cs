using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Data;

namespace NSE.Catalogo.API.Configurations
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDbContext<CatalogoContext>(options =>
                  options
                       .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                       .LogTo(Console.WriteLine)
                       .EnableDetailedErrors()
                       .EnableSensitiveDataLogging());

            return services;
        }
    }
}