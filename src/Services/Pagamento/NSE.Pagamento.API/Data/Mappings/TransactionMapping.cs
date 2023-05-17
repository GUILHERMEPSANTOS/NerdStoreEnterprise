using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Pagamento.API.Domain;

namespace NSE.Pagamento.API.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(c => c.Id);

            // 1 : N => Payment : Transaction
            builder.HasOne(c => c.Payment)
                .WithMany(c => c.Transactions);

            builder.ToTable("Transactions");
        }
    }
}