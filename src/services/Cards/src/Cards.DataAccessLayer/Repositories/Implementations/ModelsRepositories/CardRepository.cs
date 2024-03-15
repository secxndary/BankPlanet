using Cards.DataAccessLayer.Contexts;
using Cards.DataAccessLayer.Entities.Models;
using Cards.DataAccessLayer.Entities.RequestFeatures;
using Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;
using Cards.DataAccessLayer.Repositories.Extensions;
using Cards.DataAccessLayer.Repositories.Interfaces.ModelsRepositories;
using Microsoft.EntityFrameworkCore;

namespace Cards.DataAccessLayer.Repositories.Implementations.ModelsRepositories;

public class CardRepository(RepositoryContext context) : RepositoryBase<Card>(context), ICardRepository
{
    public async Task<PagedList<Card>> GetCardsAsync(CardParameters cardParameters, bool trackChanges, CancellationToken cancellationToken)
    {
        var cards = await FindAll(cardParameters.PageNumber, cardParameters.PageSize, trackChanges)
            .FilterCardsByStartDate(cardParameters.MinStartDate, cardParameters.MaxStartDate)
            .FilterCardsByExpiryDate(cardParameters.MinExpiryDate, cardParameters.MaxExpiryDate)
            .ToListAsync(cancellationToken);

        return new PagedList<Card>(cards, cardParameters.PageNumber, cardParameters.PageSize);
    }

    public async Task<Card?> GetCardByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        return await FindSingleByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public void CreateCard(Card card)
    {
        Create(card);
    }

    public void DeleteCard(Card card)
    {
        Delete(card);
    }
}