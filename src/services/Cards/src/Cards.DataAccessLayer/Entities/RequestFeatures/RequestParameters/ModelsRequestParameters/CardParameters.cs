namespace Cards.DataAccessLayer.Entities.RequestFeatures.RequestParameters.ModelsRequestParameters;

public class CardParameters : RequestParameters
{
    public DateTime MinStartDate { get; set; } = DateTime.MinValue;
    public DateTime MaxStartDate { get; set; } = DateTime.MaxValue;

    public DateTime MinExpiryDate { get; set; } = DateTime.MinValue;
    public DateTime MaxExpiryDate { get; set; } = DateTime.MaxValue;

    public bool ValidStartDate => MaxStartDate > MinStartDate;
    public bool ValidExpiryDate => MaxExpiryDate > MinExpiryDate;
}