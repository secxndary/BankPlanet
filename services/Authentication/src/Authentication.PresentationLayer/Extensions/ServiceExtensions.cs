using System.Reflection;
using System.Text;
using Authentication.BusinessLogicLayer;
using Authentication.BusinessLogicLayer.DataTransferObjects;
using Authentication.BusinessLogicLayer.Services.Implementations;
using Authentication.BusinessLogicLayer.Services.Interfaces;
using Authentication.BusinessLogicLayer.Services.Utility;
using Authentication.BusinessLogicLayer.Validators;
using Authentication.DataAccessLayer.Contexts;
using Authentication.DataAccessLayer.Entities.ConfigurationModels;
using Authentication.DataAccessLayer.Entities.Models;
using Common;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Authentication.PresentationLayer.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureIisIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options => { });

    public static void ConfigureServiceManager(this IServiceCollection services)
    {
        services.AddScoped<UserContext>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IServiceManager, ServiceManager>();
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
            options.UseSqlServer(configuration.GetConnectionString(Constants.MsSqlConnection)));

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = true;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
    }

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
            s.SwaggerDoc(Constants.V1, new OpenApiInfo { 
                Title = "BankPlanet API", 
                Version = Constants.V1,
                Description = "ASP.NET Core Web API for banking application",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Alexander Valdaitsev",
                    Email = "valdaitsevv@mail.ru",
                    Url = new Uri("https://t.me/valdaitsevv")
                },
                License = new OpenApiLicense
                {
                    Name = "BankPlanet API License",
                    Url = new Uri("https://example.com/license")
                }
            });

            s.AddSecurityDefinition(Constants.Bearer, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Enter JWT access token (e.g. Bearer eyJhbGciOiJ...)",
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