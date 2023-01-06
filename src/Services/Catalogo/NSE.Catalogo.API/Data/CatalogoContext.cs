using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Models;

namespace NSE.Catalogo.API.Data
{
    public class CatalogoContext : DbContext
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
    }
}