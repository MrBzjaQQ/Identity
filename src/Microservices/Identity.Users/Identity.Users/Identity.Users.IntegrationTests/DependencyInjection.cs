using Identity.Users.Application;
using Identity.Users.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;

namespace Identity.Users.IntegrationTests;
public static class DependencyInjection
{
    public static WebApplicationBuilder BuildTestApplication(this WebApplicationBuilder builder, DatabaseSettings databaseSettings)
    {
        builder.Services.AddValidation();
        builder.Services.AddApplicationServices();
        builder.Services.AddDatabase(databaseSettings);

        return builder;
    }
}
