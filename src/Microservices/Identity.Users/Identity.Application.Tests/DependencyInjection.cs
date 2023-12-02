using Identity.Application.Tests.Settings;
using Identity.Users.Application;
using Identity.Users.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Tests;
public static class DependencyInjection
{
    public static WebApplicationBuilder BuildTestApplication(this WebApplicationBuilder builder)
    {
        // read app settings
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var appSettings = builder.Configuration.Get<TestApplicationSettings>();

        builder.Services.AddApplicationServices();

        // Application services
        builder.Services.AddDatabase(appSettings?.Database);

        return builder;
    }
}
