using Cards.DataAccessLayer.Contexts;
using Cards.DataAccessLayer.Repositories.Interfaces;
using Cards.DataAccessLayer.Repositories.Interfaces.ModelsRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cards.DataAccessLayer.Repositories.Implementations;

public class RepositoryManager(RepositoryContext context, ICardTypeRepository cardTypeRepository, ICardRepository cardRepository, ITransactionRepository transactionRepository) 
    : IRepositoryManager
{
    public ICardTypeRepository CardType => cardTypeRepository;
    public ICardRepository Card => cardRepository;
    public ITransactionRepository Transaction => transactionRepository;

    public async Task SaveAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync(cancellationToken);

    public async Task<IDbContextTransaction> BeginSqlTransactionAsync(CancellationToken cancellationToken) =>
        await context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitSqlTransactionAsync(IDbContextTransaction dbContextTransaction, CancellationToken cancellationToken) =>
        await context.Database.CommitTransactionAsync(cancellationToken);
}