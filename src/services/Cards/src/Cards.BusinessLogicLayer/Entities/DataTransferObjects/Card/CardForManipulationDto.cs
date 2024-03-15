namespace Cards.BusinessLogicLayer.Entities.DataTransferObjects.Card;

public record CardForManipulationDto
{
    public string Number { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime ExpiryDate { get; init; }
    public decimal Balance { get; init; }
    public Guid CardTypeId { get; init; }
    public Guid UserId { get; init; }
}