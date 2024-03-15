using Cards.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cards.DataAccessLayer.Configurations;

public class CardTypeConfiguration : IEntityTypeConfiguration<CardType>
{
    public void Configure(EntityTypeBuilder<CardType> builder)
    {
        builder
            .Property(cardType => cardType.Name)
            .IsRequired()
            .HasMaxLength(300);

        builder
            .Property(cardType => cardType.Description)
            .HasMaxLength(3000);
    }
}