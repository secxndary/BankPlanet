namespace Cards.BusinessLogicLayer.Entities.DataTransferObjects.Transaction;

public record TransactionDto
{
    public Guid Id { get; init; }
    public decimal Amount { get; init; }
    public DateTime Timestamp { get; init; }
    public Guid SenderCardId { get; init; }
    public Guid RecipientCardId { get; init; }
}