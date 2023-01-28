using Microsoft.EntityFrameworkCore;
using NSE.Carrinho.Api.Data;

namespace NSE.Carrinho.Api.Configurations
{
    public static class DbContexConfig
    {
        public static IServiceCollection AddDbContextConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<ShoppingCartContext>(options =>
                options
                    .UseSqlServer("DefaultConnection")
                    .EnableDetailedErrors()                
            );

            return services;
        }
    }
}