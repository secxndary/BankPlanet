using Cards.DataAccessLayer.Entities.Models;

namespace Cards.DataAccessLayer.Repositories.Extensions;

public static class CardRepositoryExtensions
{
    public static IQueryable<Card> FilterCardsByStartDate(this IQueryable<Card> cards, DateTime minStartDate, DateTime maxStartDate)
    {
        return cards.Where(c => c.StartDate >= minStartDate && c.StartDate <= maxStartDate);
    }

    public static IQueryable<Card> FilterCardsByExpiryDate(this IQueryable<Card> cards, DateTime minExpiryDate, DateTime maxExpiryDate)
    {
        return cards.Where(c => c.ExpiryDate >= minExpiryDate && c.ExpiryDate <= maxExpiryDate);
    }
}