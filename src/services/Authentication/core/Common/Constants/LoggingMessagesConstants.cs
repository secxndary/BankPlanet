namespace Common.Constants;

public static class LoggingMessagesConstants
{
    public const string RegisterUserAsyncError = $"{ErrorMessage} User registration. {StackTrace}";
    public const string RegisterUserAsyncSuccess = $"User registered {Successfully}.";

    public const string ValidateUserAsyncError = $"{ErrorMessage} User Validation. {StackTrace}";
    public const string ValidateUserAsyncSuccess = $"User validated {Successfully}.";

    public const string CreateTokenAsyncError = $"{ErrorMessage} Token Creation. {StackTrace}";
    public const string CreateTokenAsyncSuccess = $"Token created {Successfully}.";
    
    public const string RefreshTokenAsyncError = $"{ErrorMessage} Token Refresh. {StackTrace}";
    public const string RefreshTokenAsyncSuccess = $"Token refreshed {Successfully}.";
    
    private const string ErrorMessage = "Error occured during"; 
    private const string StackTrace = "See the Stack trace.";
    private const string Successfully = "successfully";
}