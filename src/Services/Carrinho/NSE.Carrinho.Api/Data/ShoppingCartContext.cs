using Microsoft.EntityFrameworkCore;

namespace NSE.Carrinho.Api.Data
{
    public class ShoppingCartContext : DbContext
    {
        public ShoppingCartContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
        }


        private void mapForgottenProperties(ModelBuilder builder)
        {
            var properties = builder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));

            foreach (var property in properties)
            {
                property.SetColumnType("Varchar(100)");
            }
        }
    }
}