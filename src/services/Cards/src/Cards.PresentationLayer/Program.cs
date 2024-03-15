using Cards.PresentationLayer.Extensions;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureLowercaseRoute();
builder.Services.ConfigureIisIntegration();

builder.Services.AddMediatR(typeof(Cards.BusinessLogicLayer.AssemblyReference).Assembly);
builder.Services.AddAutoMapper(typeof(Cards.BusinessLogicLayer.AssemblyReference).Assembly);

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.ConfigureControllers();
builder.Services.ConfigureFluentValidation();

builder.Services.ConfigureLoggerManager();
builder.Services.ConfigureLogging();
builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.Run();