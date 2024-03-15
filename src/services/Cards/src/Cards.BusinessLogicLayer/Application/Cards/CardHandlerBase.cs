using Cards.BusinessLogicLayer.Entities.Exceptions.MessagesConstants;
using Cards.BusinessLogicLayer.Entities.Exceptions.NotFound;
using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Repositories.Interfaces;

namespace Cards.BusinessLogicLayer.Application.Cards;

public abstract class CardHandlerBase(IRepositoryManager repository)
{
    protected async Task<Card> GetCardAndCheckIfItExistsAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        var card = await repository.Card.GetCardByIdAsync(id, trackChanges, cancellationToken);

        if (card is null)
        {
            throw new CardNotFoundException(CardExceptionMessages.CardNotFound);
        }

        return card;
    }

    protected async Task CheckIfCardTypeExistsAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        var cardType = await repository.CardType.GetCardTypeByIdAsync(id, trackChanges, cancellationToken);

        if (cardType is null)
        {
            throw new CardTypeNotFoundException(CardTypeExceptionMessages.CardTypeNotFound);
        }
    }
}