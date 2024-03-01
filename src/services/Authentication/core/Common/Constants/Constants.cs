namespace Common.Constants;

public static class Constants
{
    public const string ApplicationJson = "application/json";
    public const string AppSettings = "appsettings";
    public const string Json = "json";
    public const string AppSettingsJson = $"{AppSettings}.{Json}";

    public const string MsSqlConnection = "mssqlConnection";
    public const string V1 = "v1";

    public const string Secret = "SECRET";
    public const string JwtSettings = "JwtSettings";
    public const string Authorization = "Authorization";
    public const string Bearer = "Bearer";

    public const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    public const string Environment = "Environment";

    public const string ElasticConfigurationUri = "ElasticConfiguration:Uri";
    public const int NumberOfReplicas = 1;
    public const int NumberOfShards = 2;
}