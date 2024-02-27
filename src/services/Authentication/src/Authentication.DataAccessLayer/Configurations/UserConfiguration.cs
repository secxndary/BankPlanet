using Authentication.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.DataAccessLayer.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(u => u.FirstName)
            .HasMaxLength(100);

        builder
            .Property(u => u.LastName)
            .HasMaxLength(100);

        builder
            .Property(u => u.UserName)
            .HasMaxLength(50);
    }
}