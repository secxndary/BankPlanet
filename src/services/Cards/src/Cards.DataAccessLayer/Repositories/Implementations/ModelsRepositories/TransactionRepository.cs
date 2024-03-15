using Cards.DataAccessLayer.Contexts;
using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;
using Cards.DataAccessLayer.Repositories.Extensions;
using Cards.DataAccessLayer.Repositories.Interfaces.ModelsRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cards.DataAccessLayer.Repositories.Implementations.ModelsRepositories;

public class TransactionRepository(RepositoryContext context) : RepositoryBase<Transaction>(context), ITransactionRepository
{
    public async Task<PagedList<Transaction>> GetTransactionsAsync(TransactionParameters transactionParameters, bool trackChanges, CancellationToken cancellationToken)
    {
        var transactions = await FindAll(transactionParameters.PageNumber, transactionParameters.PageSize, trackChanges)
            .FilterTransactionsByTimestamp(transactionParameters.MinTimestamp, transactionParameters.MaxTimestamp)
            .ToListAsync(cancellationToken);

        return new PagedList<Transaction>(transactions, transactionParameters.PageNumber, transactionParameters.PageSize);
    }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        return await FindSingleByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public void CreateTransaction(Transaction transaction)
    {
        Create(transaction);
    }

    public void DeleteTransaction(Transaction transaction)
    {
        Delete(transaction);
    }
}