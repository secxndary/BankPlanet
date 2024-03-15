namespace Cards.DataAccessLayer.Entities.Models;

public class CardType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}