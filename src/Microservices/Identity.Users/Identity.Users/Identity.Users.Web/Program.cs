using Identity.Users.Application;
using Identity.Users.Infrastructure.Database;
using Identity.Users.Resources.Constants;
using Identity.Users.Web.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var applicationSettings = builder.Configuration.Get<WebAppSettings>();
builder.Services.AddDatabase(applicationSettings?.DatabaseSettings);
builder.Services.AddApplicationServices();

var app = builder.Build();

await app.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation(ApplicationStartup.ApplicationStarted);
app.Run();
