namespace Cards.DataAccessLayer.Entities.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }

    public Guid SenderCardId { get; set; }
    public Card? SenderCard { get; set; }

    public Guid RecipientCardId { get; set; }
    public Card? RecipientCard { get; set; }
}