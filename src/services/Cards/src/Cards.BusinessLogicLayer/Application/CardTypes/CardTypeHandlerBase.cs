using Cards.BusinessLogicLayer.Entities.Exceptions.MessagesConstants;
using Cards.BusinessLogicLayer.Entities.Exceptions.NotFound;
using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Repositories.Interfaces;

namespace Cards.BusinessLogicLayer.Application.CardTypes;

public abstract class CardTypeHandlerBase(IRepositoryManager repository)
{
    protected async Task<CardType> GetCardTypeAndCheckIfItExistsAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        var cardType = await repository.CardType.GetCardTypeByIdAsync(id, trackChanges, cancellationToken);

        if (cardType is null)
        {
            throw new CardTypeNotFoundException(CardTypeExceptionMessages.CardTypeNotFound);
        }

        return cardType;
    }
}