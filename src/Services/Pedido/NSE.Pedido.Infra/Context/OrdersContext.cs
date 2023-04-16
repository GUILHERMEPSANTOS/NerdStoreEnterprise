using Core.Data;
using Core.Mediator;
using Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NSE.Pedido.Domain.Orders;
using NSE.Pedido.Domain.Vouchers;

namespace NSE.Pedido.Infra.Context
{
    public class OrdersContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediator;
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public OrdersContext(DbContextOptions options, IMediatorHandler mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
            IgnoreProperties(builder);
            MapForgottenProperties(builder);

            builder.HasSequence<int>("OrderCodeSequence").StartsAt(1000).IncrementsBy(1);
        }

        private void IgnoreProperties(ModelBuilder builder)
        {
            builder.Ignore<Event>();
            builder.Ignore<ValidationResult>();
        }

        private void MapForgottenProperties(ModelBuilder builder)
        {
            var entitiesTypes = GetEntityTypes(builder);

            var properties = entitiesTypes
                    .SelectMany(e => e.GetProperties())
                    .Where(p => p.ClrType == typeof(string));

            foreach (var property in properties)
            {
                property.SetColumnType("VARCHAR(100)");
            }
        }

        private void OnDelete(ModelBuilder builder)
        {
            var entitiesTypes = GetEntityTypes(builder);
            var foreignKeys = GetForeignKeys(entitiesTypes);

            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.SetNull;
            }

        }

        public async Task<bool> Commit()
        {
            UpdateDateAdded();

            var success = await base.SaveChangesAsync() > 0;

            return success;
        }

        private void UpdateDateAdded()
        {

            var entityEntries = ChangeTracker.Entries()
                .Where(entry => entry.GetType().GetProperty("DateAdded") != null);


            foreach (var entity in entityEntries)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Property("DateAdded").CurrentValue = DateTime.Now;
                }

                if (entity.State == EntityState.Unchanged)
                {
                    entity.Property("DateAdded").IsModified = false;
                }
            }
        }

        private IEnumerable<IMutableEntityType> GetEntityTypes(ModelBuilder builder)
        {
            return builder
                    .Model
                    .GetEntityTypes();
        }

        private IEnumerable<IMutableForeignKey> GetForeignKeys(IEnumerable<IMutableEntityType> mutableEntities)
        {
            return mutableEntities.SelectMany(entity => entity.GetForeignKeys());
        }
    }
}