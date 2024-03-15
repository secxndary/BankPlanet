namespace Cards.BusinessLogicLayer.Entities.Exceptions.NotFound;

public class TransactionNotFoundException(string message) : NotFoundException(message);