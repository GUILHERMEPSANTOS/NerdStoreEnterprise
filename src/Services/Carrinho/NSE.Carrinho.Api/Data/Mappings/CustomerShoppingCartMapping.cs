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

            builder.Ignore(shoppingCart => shoppingCart.Voucher)
                .OwnsOne(shoppingCart => shoppingCart.Voucher, voucherBuilder =>
                {
                    voucherBuilder.Property(voucher => voucher.Code)
                                  .HasColumnName("VoucherCode")
                                  .HasMaxLength(50)
                                  .IsRequired(false);

                    voucherBuilder.Property(voucher => voucher.DiscountType);                            ;

                    voucherBuilder.Property(voucher => voucher.Percentage)
                                  .HasColumnName("VoucherPercentage");

                    voucherBuilder.Property(voucher => voucher.Discount)
                                  .HasColumnName("VoucherDiscount");
                });
        }
    }
}