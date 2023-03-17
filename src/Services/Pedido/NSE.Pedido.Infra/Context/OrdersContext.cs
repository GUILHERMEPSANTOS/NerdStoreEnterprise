using Core.Data;
using Core.Mediator;
using Core.Messages;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NSE.Pedido.Domain.Vouchers;

namespace NSE.Pedido.Infra.Context
{
    public class OrdersContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediator;
        public DbSet<Voucher> Vouchers { get; set; }

        public OrdersContext(DbContextOptions options, IMediatorHandler mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrdersContext).Assembly);
            IgnoreProperties(modelBuilder);
            MapForgottenProperties(modelBuilder);
        }

        private void IgnoreProperties(ModelBuilder builder)
        {
            builder.Ignore<Event>();
            builder.Ignore<ValidationResult>();
        }

        private void MapForgottenProperties(ModelBuilder modelBuilder)
        {
            var properties = modelBuilder
                    .Model
                    .GetEntityTypes()
                    .SelectMany(e => e.GetProperties())
                    .Where(p => p.ClrType == typeof(string));

            foreach (var property in properties)
            {
                property.SetColumnType("VARCHAR(100)");
            }
        }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;

            return success;
        }
    }
}