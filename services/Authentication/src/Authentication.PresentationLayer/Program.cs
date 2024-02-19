using Authentication.BusinessLogicLayer;
using Authentication.PresentationLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureLowercaseRoute();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.AddAutoMapper(typeof(AssemblyReference).Assembly);
builder.Services.ConfigureServiceManager();

builder.Services.ConfigureIisIntegration();
builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddControllers();


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