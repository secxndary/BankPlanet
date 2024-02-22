namespace Authentication.DataAccessLayer.Entities.Exceptions.NotFound;

public class UserNotFoundException(string message) : NotFoundException(message);