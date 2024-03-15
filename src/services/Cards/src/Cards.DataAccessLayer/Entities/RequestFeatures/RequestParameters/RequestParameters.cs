using Common.Constants;

namespace Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters;

public abstract class RequestParameters
{
    private const int MaxPageSize = Constants.DefaultMaxPageSize;
    private int _pageSize = Constants.DefaultPageSize;

    public int PageNumber { get; set; } = 1;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
    public string? OrderBy { get; set; }
}