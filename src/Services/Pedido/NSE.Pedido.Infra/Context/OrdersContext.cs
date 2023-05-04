using Core.Data;
using Core.DomainObjects;
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

            var sucess = await base.SaveChangesAsync() > 0;
           
            if (sucess) await _mediator.PublishEvents(this);

            return sucess;
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

                if (entity.State == EntityState.Modified)
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

    public static class MediatorExtensions
    {
        public static async Task PublishEvents<TContext>(this IMediatorHandler mediator, TContext context) where TContext : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(entry => entry.Entity.Notification is not null && entry.Entity.Notification.Any());

            if (domainEntities is not null)
            {
                var domainEvents = domainEntities
                    .SelectMany(entry => entry.Entity.Notification)
                    .ToList();

                domainEntities?.ToList()
                    .ForEach(entry => entry.Entity.ClearEvents());


                foreach (var @event in domainEvents)
                {
                    await mediator.PublishEvent(@event);
                }
            }
        }
    }

}