using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Cliente.API.Domain.Entities;

namespace NSE.Cliente.API.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Street)
                .IsRequired()
                .HasColumnType("VARCHAR(200)");

            builder.Property(a => a.HouseNumber)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasColumnType("VARCHAR(20)");

            builder.Property(a => a.Complement)
                .IsRequired()
                .HasColumnType("VARCHAR(250)");

            builder.Property(a => a.Neighborhood)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(a => a.City)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(a => a.State)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.ToTable("Adresses");
        }
    }
}