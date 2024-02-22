namespace Authentication.DataAccessLayer.Entities.Exceptions.MessagesConstants;

public static class ExceptionMessagesConstants
{
    public const string RefreshTokenBadRequest = "Invalid client request: token has invalid values.";
    public const string Unauthorized = "Incorrect credentials";
    public const string SecurityToken = "Invalid token";
    public const string UserNotFound = "This username was not found in the database";
}