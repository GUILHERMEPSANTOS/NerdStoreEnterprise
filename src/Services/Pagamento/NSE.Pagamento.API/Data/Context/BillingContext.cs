using Core.Data;
using Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Pagamento.API.Domain;

namespace NSE.Pagamento.API.Data.Context
{
    public class BillingContext : DbContext, IUnitOfWork
    {
        public BillingContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            return await SaveChangesAsync() > 0;
        }
    }
}