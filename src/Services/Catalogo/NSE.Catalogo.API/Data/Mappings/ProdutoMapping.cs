

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Catalogo.API.Domain.Entities;

namespace NSE.Catalogo.API.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(produto => produto.Id);

            builder.Property(produto => produto.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(produto => produto.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(produto => produto.Image)
                .IsRequired()
                .HasColumnType("VARCHAR(250)");
        }
    }
}


