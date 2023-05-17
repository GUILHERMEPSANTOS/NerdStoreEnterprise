
using Microsoft.EntityFrameworkCore;
using NSE.Pagamento.API.Data.Context;

namespace NSE.Pagamento.API.Configurations
{
    public static class DbContextConfig
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BillingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}