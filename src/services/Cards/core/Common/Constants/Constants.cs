namespace Common.Constants;

public static class Constants
{
    public const string ApplicationJson = "application/json";
    public const string AppSettings = "appsettings";
    public const string Json = "json";
    public const string AppSettingsJson = $"{AppSettings}.{Json}";

    public const string PostgresConnection = "PostgresConnection";

    public const string Secret = "SECRET";
    public const string JwtSettings = "JwtSettings";
    public const string Authorization = "Authorization";
    public const string Bearer = "Bearer";
    public const string SecurityDefinitionDescription = "Enter JWT access token (e.g. Bearer eyJhbGciOiJ...)";

    public const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    public const string Environment = "Environment";

    public const string ElasticConfigurationUri = "ElasticConfiguration:Uri";
    public const int NumberOfReplicas = 1;
    public const int NumberOfShards = 2;

    public const string ApiController = "api/[controller]";
    public const string PaginationHeader = "X-Pagination";

    public const string CardTypesRoute = "api/card-types";
    
    public const string GetCardById = "GetCardById";
    public const string GetCardTypeById = "GetCardTypeById";
    public const string GetTransactionById = "GetTransactionById";

    public const int DefaultPageSize = 10;
    public const int DefaultMaxPageSize = 50;
}