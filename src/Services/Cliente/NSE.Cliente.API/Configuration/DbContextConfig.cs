using NSE.Cliente.API.Data;
using Microsoft.EntityFrameworkCore;

namespace NSE.Cliente.API.Configuration
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDbContext<CustomerContext>(
                options => options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                    .EnableDetailedErrors()
            );

            return services;
        }

    }
}