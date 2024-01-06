using Identity.Users.Application;
using Microsoft.AspNetCore.Builder;
using Refit;
using Microsoft.Extensions.DependencyInjection;
using Identity.Users.ApiTests.Services;
using System.Text.Json.Serialization;
using System.Text.Json;
using DateOnlyTimeOnly.AspNet.Converters;

namespace Identity.Users.ApiTests;

public static class DependencyInjection
{
    public static WebApplicationBuilder BuildTestApplication(this WebApplicationBuilder builder, string applicationEndpoint)
    {
        // read app settings
        builder.Services.AddApplicationServices();

        // Application services
        builder.Services.ConfigureUsersService(applicationEndpoint);

        return builder;
    }

    /// <summary>
    /// Method configures API client service using Refit
    /// </summary>
    /// <param name="services">Collection of app services</param>
    /// <param name="applicationEndpoint">URL of the container which has an application to be tested.</param>
    /// <returns>Collection of app services</returns>
    public static IServiceCollection ConfigureUsersService(this IServiceCollection services, string applicationEndpoint)
    {
        var settings = new RefitSettings();
        settings.ContentSerializer = new SystemTextJsonContentSerializer(
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                    new DateOnlyJsonConverter()
                }
            });

        services
            .AddRefitClient<IUsersEndpointsService>(settings)
            .ConfigureHttpClient(configure =>
            {
                configure.BaseAddress = new Uri(applicationEndpoint);
                configure.Timeout = TimeSpan.FromSeconds(180);
            });

        return services;
    }
}