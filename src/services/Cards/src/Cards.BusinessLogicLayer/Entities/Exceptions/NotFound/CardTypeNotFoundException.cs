namespace Cards.BusinessLogicLayer.Entities.Exceptions.NotFound;

public class CardTypeNotFoundException(string message) : NotFoundException(message);