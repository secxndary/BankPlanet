namespace Cards.BusinessLogicLayer.Entities.Exceptions.BadRequest;

public abstract class BadRequestException(string message) : Exception(message);