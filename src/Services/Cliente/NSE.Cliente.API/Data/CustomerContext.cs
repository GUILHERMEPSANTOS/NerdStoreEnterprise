using Core.Data;
using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Domain;

namespace NSE.Cliente.API.Data
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public CustomerContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);

            MapForgottenProperties(modelBuilder);

            DisableCascadeDelete(modelBuilder);
        }

        private void MapForgottenProperties(ModelBuilder modelBuilder)
        {
            var properties = modelBuilder.Model.GetEntityTypes()
                    .SelectMany(properties => properties.GetProperties().Where(property => properties.ClrType == typeof(string)));

            foreach (var property in properties)
            {
                property.SetColumnType("VARCHAR(100)");
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
            var success = await base.SaveChangesAsync() > 0;

            return success;
        }
    }
}