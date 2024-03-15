using Cards.DataAccessLayer.Contexts;
using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;
using Cards.DataAccessLayer.Repositories.Extensions;
using Cards.DataAccessLayer.Repositories.Interfaces.ModelsRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cards.DataAccessLayer.Repositories.Implementations.ModelsRepositories;

public class CardTypeRepository(RepositoryContext context) : RepositoryBase<CardType>(context), ICardTypeRepository
{
    public async Task<PagedList<CardType>> GetCardTypesAsync(CardTypeParameters cardTypeParameters, bool trackChanges, CancellationToken cancellationToken)
    {
        var cardTypes = await FindAll(cardTypeParameters.PageNumber, cardTypeParameters.PageSize, trackChanges)
            .ToListAsync(cancellationToken);

        return new PagedList<CardType>(cardTypes, cardTypeParameters.PageNumber, cardTypeParameters.PageSize);
    }

    public async Task<CardType?> GetCardTypeByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        return await FindSingleByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public void CreateCardType(CardType cardType)
    {
        Create(cardType);
    }

    public void DeleteCardType(CardType cardType)
    {
        Delete(cardType);
    }
}