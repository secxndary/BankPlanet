using Cards.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.DataAccessLayer.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder
            .Property(c => c.Number)
            .IsRequired()
            .HasMaxLength(20);

        builder
            .Property(c => c.Balance)
            .IsRequired();

        builder
            .Property(c => c.StartDate)
            .IsRequired();

        builder
            .Property(c => c.ExpiryDate)
            .IsRequired();

        builder
            .Property(c => c.CardTypeId)
            .IsRequired();

        builder
            .Property(c => c.UserId)
            .IsRequired();
    } 
}