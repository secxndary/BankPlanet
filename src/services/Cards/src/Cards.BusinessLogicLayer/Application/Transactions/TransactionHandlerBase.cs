using Cards.BusinessLogicLayer.Entities.Exceptions.MessagesConstants;
using Cards.BusinessLogicLayer.Entities.Exceptions.NotFound;
using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Repositories.Interfaces;

namespace Cards.BusinessLogicLayer.Application.Transactions;

public abstract class TransactionHandlerBase(IRepositoryManager repository)
{
    protected async Task<Transaction> GetTransactionAndCheckIfItExistsAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        var transaction = await repository.Transaction.GetTransactionByIdAsync(id, trackChanges, cancellationToken);

        if (transaction is null)
        {
            throw new TransactionNotFoundException(TransactionExceptionMessages.TransactionNotFound);
        }

        return transaction;
    }

    protected async Task CheckIfCardExistsAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        var card = await repository.Card.GetCardByIdAsync(id, trackChanges, cancellationToken);

        if (card is null)
        {
            throw new CardNotFoundException(CardExceptionMessages.CardNotFound);
        }
    }

    protected async Task<Card> GetCardAndCheckIfItExistsAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        var card = await repository.Card.GetCardByIdAsync(id, trackChanges, cancellationToken);

        if (card is null)
        {
            throw new CardNotFoundException(CardExceptionMessages.CardNotFound);
        }

        return card;
    }
}