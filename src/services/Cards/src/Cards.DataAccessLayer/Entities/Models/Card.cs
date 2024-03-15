namespace Cards.DataAccessLayer.Entities.Models;

public class Card
{
    public Guid Id { get; set; }
    public string Number { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal Balance { get; set; }

    public Guid CardTypeId { get; set; }
    public CardType? CardType { get; set; }

    public Guid UserId { get; set; }
    // TODO https://app.clickup.com/t/86942bkje add User via replication
}