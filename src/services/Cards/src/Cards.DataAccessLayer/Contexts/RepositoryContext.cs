using Cards.DataAccessLayer.Configurations;
using Cards.DataAccessLayer.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.DataAccessLayer.Contexts;

public class RepositoryContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(CardConfiguration).Assembly);
    }

    public DbSet<CardType>? CardTypes { get; set; }
    public DbSet<Card>? Cards { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }
}