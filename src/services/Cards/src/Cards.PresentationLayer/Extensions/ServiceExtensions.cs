using System.Reflection;
using System.Text;
using Cards.BusinessLogicLayer;
using Cards.DataAccessLayer.Contexts;
using Cards.DataAccessLayer.Entities.ConfigurationModels;
using Cards.DataAccessLayer.Repositories.Implementations;
using Cards.DataAccessLayer.Repositories.Implementations.ModelsRepositories;
using Cards.DataAccessLayer.Repositories.Interfaces;
using Cards.DataAccessLayer.Repositories.Interfaces.ModelsRepositories;
using Common.Constants;
using Common.Logging;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Cards.PresentationLayer.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureIisIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(_ => { });

    public static void ConfigureLoggerManager(this IServiceCollection services)
    {
        services.AddSingleton(Log.Logger);
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<ICardTypeRepository, CardTypeRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureLogging(this IServiceCollection _)
    {
        var environment = Environment.GetEnvironmentVariable(Constants.AspNetCoreEnvironment);
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(Constants.AppSettingsJson, optional: false, reloadOnChange: true)
            .AddJsonFile($"{Constants.AppSettings}.{environment}.{Constants.Json}", optional: true)
            .Build();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment!))
            .Enrich.WithProperty(Constants.Environment, environment)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration[Constants.ElasticConfigurationUri]!))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
            NumberOfReplicas = Constants.NumberOfReplicas,
            NumberOfShards = Constants.NumberOfShards
        };
    }

    public static void ConfigureLowercaseRoute(this IServiceCollection services) =>
        services.AddRouting(options => options.LowercaseUrls = true);

    public static void ConfigureFluentValidation(this IServiceCollection services) =>
        services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(Constants.PostgresConnection)));

    public static void ConfigureControllers(this IServiceCollection services) =>
        services.AddControllers();

    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration();
        configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

        var secretKey = Environment.GetEnvironmentVariable(Constants.Secret);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = jwtConfiguration.ValidateLifetime,

                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });
    }

    public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<JwtConfiguration>(configuration.GetSection(Constants.JwtSettings));

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.AddSecurityDefinition(Constants.Bearer, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = Constants.SecurityDefinitionDescription,
                Name = Constants.Authorization,
                Type = SecuritySchemeType.ApiKey,
                Scheme = Constants.Bearer
            });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = Constants.Bearer
                        },
                        Name = Constants.Bearer,
                    },
                    new List<string>()
                }
            });
        });
    }
}