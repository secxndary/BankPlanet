using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;

namespace Cards.DataAccessLayer.Repositories.Interfaces.ModelsRepositories;

public interface ICardTypeRepository
{
    Task<PagedList<CardType>> GetCardTypesAsync(CardTypeParameters cardTypeParameters, bool trackChanges, CancellationToken cancellationToken);
    Task<CardType?> GetCardTypeByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken);
    void CreateCardType(CardType cardType);
    void DeleteCardType(CardType cardType);
}