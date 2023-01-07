using Core.Data;
using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Domain.Entities;

namespace NSE.Catalogo.API.Data
{
    public class CatalogoContext : DbContext, IUnitOfWork
    {
        public DbSet<Produto> Produtos;
        public CatalogoContext(DbContextOptions<CatalogoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);

            MapForgottenProperties(builder);
        }
        private void MapForgottenProperties(ModelBuilder builder)
        {
            foreach (var property in builder
                                        .Model
                                        .GetEntityTypes()
                                        .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))

                property.SetColumnType("Varchar(100)");
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}