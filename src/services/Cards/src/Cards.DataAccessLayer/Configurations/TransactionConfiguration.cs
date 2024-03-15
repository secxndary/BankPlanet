using Cards.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.DataAccessLayer.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .Property(t => t.Amount)
            .IsRequired();

        builder
            .Property(t => t.Timestamp)
            .IsRequired();

        builder
            .Property(t => t.SenderCardId)
            .IsRequired();

        builder
            .Property(t => t.RecipientCardId)
            .IsRequired();
    }
}