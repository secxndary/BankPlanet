namespace Cards.DataAccessLayer.Entities.RequestFeatures;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }

    public PagedList(IEnumerable<T> items, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            TotalCount = items.Count(),
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(items.Count() / (double)pageSize)
        };

        AddRange(items);
    }
}