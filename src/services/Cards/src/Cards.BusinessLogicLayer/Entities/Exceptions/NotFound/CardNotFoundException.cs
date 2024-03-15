namespace Cards.BusinessLogicLayer.Entities.Exceptions.NotFound;

public class CardNotFoundException(string message) : NotFoundException(message);