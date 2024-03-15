using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;

namespace Cards.DataAccessLayer.Repositories.Interfaces.ModelsRepositories;

public interface ITransactionRepository
{
    Task<PagedList<Transaction>> GetTransactionsAsync(TransactionParameters transactionParameters, bool trackChanges, CancellationToken cancellationToken);
    Task<Transaction?> GetTransactionByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken);
    void CreateTransaction(Transaction transaction);
    void DeleteTransaction(Transaction transaction);
}