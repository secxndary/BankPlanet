namespace Cards.BusinessLogicLayer.Entities.Exceptions.BadRequest;

public class InsufficientFundsException(string message) : BadRequestException(message);