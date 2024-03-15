using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;

namespace Cards.DataAccessLayer.Repositories.Interfaces.ModelsRepositories;

public interface ICardRepository
{
    Task<PagedList<Card>> GetCardsAsync(CardParameters cardParameters, bool trackChanges, CancellationToken cancellationToken);
    Task<Card?> GetCardByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken);
    void CreateCard(Card card);
    void DeleteCard(Card card);
}