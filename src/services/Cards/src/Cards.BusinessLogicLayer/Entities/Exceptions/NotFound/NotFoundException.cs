namespace Cards.BusinessLogicLayer.Entities.Exceptions.NotFound;

public abstract class NotFoundException(string message) : Exception(message);