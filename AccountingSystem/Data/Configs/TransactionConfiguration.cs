
using AccountingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Protocols;

namespace AccountingSystem.Data.Configs
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Date);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreditAccount).IsRequired();
            builder.Property(x => x.DebitAccount).IsRequired();
            builder.Property(x => x.Ammount).IsRequired().HasPrecision(18,2);

        }
    }
}
