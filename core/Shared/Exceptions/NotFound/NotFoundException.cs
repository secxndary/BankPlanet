namespace Shared.Exceptions.NotFound;

public abstract class NotFoundException(string message) : Exception(message);