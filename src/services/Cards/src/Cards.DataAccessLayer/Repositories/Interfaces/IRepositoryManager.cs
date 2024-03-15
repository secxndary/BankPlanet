using Cards.DataAccessLayer.Repositories.Interfaces.ModelsRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cards.DataAccessLayer.Repositories.Interfaces;

public interface IRepositoryManager
{
    ICardTypeRepository CardType { get; }
    ICardRepository Card { get; }
    ITransactionRepository Transaction { get; }

    Task SaveAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginSqlTransactionAsync(CancellationToken cancellationToken);
    Task CommitSqlTransactionAsync(IDbContextTransaction dbContextTransaction, CancellationToken cancellationToken);
}