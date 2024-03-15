namespace Cards.BusinessLogicLayer.Entities.DataTransferObjects.CardType;

public record CardTypeDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
}