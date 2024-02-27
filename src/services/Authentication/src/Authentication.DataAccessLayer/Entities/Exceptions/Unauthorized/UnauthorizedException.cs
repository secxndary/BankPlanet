namespace Authentication.DataAccessLayer.Entities.Exceptions.Unauthorized;

public class UnauthorizedException(string message) : Exception(message);