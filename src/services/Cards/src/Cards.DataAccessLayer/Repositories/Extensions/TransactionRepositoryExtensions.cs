using Cards.DataAccessLayer.Entities.Models;

namespace Cards.DataAccessLayer.Repositories.Extensions;

public static class TransactionRepositoryExtensions
{
    public static IQueryable<Transaction> FilterTransactionsByTimestamp(this IQueryable<Transaction> transactions, DateTime minTimestamp, DateTime maxTimestamp)
    {
        return transactions.Where(c => c.Timestamp >= minTimestamp && c.Timestamp <= maxTimestamp);
    }
}