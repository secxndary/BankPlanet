namespace Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;

public class TransactionParameters : RequestParameters
{
    public DateTime MinTimestamp { get; set; } = DateTime.MinValue;
    public DateTime MaxTimestamp { get; set; } = DateTime.MaxValue;

    public bool ValidTimestamp => MaxTimestamp > MinTimestamp;
}