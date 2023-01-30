using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Carrinho.Api.Domain;

namespace NSE.Carrinho.Api.Data.Mappings
{
    public class CustomerShoppingCartMapping : IEntityTypeConfiguration<CustomerShoppingCart>
    {
        public void Configure(EntityTypeBuilder<CustomerShoppingCart> builder)
        {

            builder
                .HasIndex(c => c.CustomerId)
                .HasDatabaseName("idx_cliente");

            builder
                .HasMany(c => c.Items)
                .WithOne(i => i.CustomerShoppingCart)
                .HasForeignKey(i => i.ShoppingCartId);

        }
    }
}