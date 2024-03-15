namespace Cards.BusinessLogicLayer.Entities.DataTransferObjects.CardType;

public record CardTypeForManipulationDto
{
    public string Name { get; init; }
    public string? Description { get; init; }
}