using Core.Data;
using Core.DomainObjects;
using Core.Mediator;
using Microsoft.EntityFrameworkCore;
using NSE.Cliente.API.Domain.Entities;

namespace NSE.Cliente.API.Data;
public class CustomerContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediator;
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public CustomerContext(DbContextOptions options, IMediatorHandler mediator) : base(options)
    {
        _mediator = mediator;
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

        if(success) await _mediator.PublishEvents<CustomerContext>(this);

        return success;
    }
}


public static class MediatorExtensions
{
    public static async Task PublishEvents<TContext>(this IMediatorHandler mediator, TContext context) where TContext : DbContext
    {
        var domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Notification != null && x.Entity.Notification.Any());

        var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notification);

        domainEntities.ToList()
            .ForEach(x => x.Entity.ClearEvents());

        foreach (var @event in domainEvents)
        {
            await mediator.PublishEvent(@event);
        }
    }
}

