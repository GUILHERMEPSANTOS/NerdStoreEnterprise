using Core.Data;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Carrinho.Api.Domain;

namespace NSE.Carrinho.Api.Data
{
    public class ShoppingCartContext : DbContext, IUnitOfWork
    {
        public DbSet<CustomerShoppingCart> CustomerShoppingCarts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public ShoppingCartContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ShoppingCartContext).Assembly);
            builder.Ignore<ValidationResult>();

            MapForgottenProperties(builder);
            DisableCascadeDelete(builder);
        }


        private void MapForgottenProperties(ModelBuilder builder)
        {
            var properties = builder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));

            foreach (var property in properties)
            {
                property.SetColumnType("Varchar(100)");
            }
        }

        private void DisableCascadeDelete(ModelBuilder modelBuilder)
        {
            var properties = modelBuilder.Model.GetEntityTypes()
                .SelectMany(properties => properties.GetForeignKeys());

            foreach (var property in properties)
            {
                property.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}