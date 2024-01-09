using DotNet.Testcontainers.Builders;
using Identity.Users.Application.Services;
using Identity.Users.Infrastructure.Database;
using Identity.Users.IntegrationTests.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Identity.Users.IntegrationTests;

[SetUpFixture]
public class SetUpTests
{
    private const string SettingsAreNull = "Database container settings are NULL";
    private const string ContainerStartedLog = "PostgreSQL init process complete; ready for start up.";

    [OneTimeSetUp]
    public async Task RunBeforeTests()
    {
        var appSettings = GetApplicationSettings();
        var container = CreateDbContainer(appSettings?.DbContainer);
        TestModule.Current.SetDbContainer(container);
        await TestModule.Current.StartDbContainerAsync();

        await using var serviceProvider = BuildServiceProvider();
        var dbMigrator = serviceProvider.GetRequiredService<IUsersDbMigrator>();
        await dbMigrator.MigrateAsync();
        await dbMigrator.SeedDataAsync();
    }

    [OneTimeTearDown]
    public async Task RunAfterTests()
    {
        await using var serviceProvider = BuildServiceProvider();
        var dbMigrator = serviceProvider.GetRequiredService<IUsersDbMigrator>();
        await dbMigrator.EnsureDatabaseDeletedAsync();

        await TestModule.Current.StopDbContainerAsync();
    }

    private static TestApplicationSettings? GetApplicationSettings()
    {
        var tempBuilder = new ConfigurationBuilder();
        tempBuilder.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true);
        var appConfiguration = tempBuilder.Build();
        return appConfiguration.Get<TestApplicationSettings>();
    }

    private static PostgreSqlContainer CreateDbContainer(DatabaseContainerSettings? containerSettings)
    {
        if (containerSettings is null)
            throw new ArgumentException(SettingsAreNull);

        var postgreSqlBuilder = new PostgreSqlBuilder()
            .WithHostname(containerSettings.Host)
            .WithImage(PostgreSqlBuilder.PostgreSqlImage)
            .WithDatabase(containerSettings.DatabaseName)
            .WithUsername(containerSettings.Username)
            .WithPassword(containerSettings.Password)
            .WithPortBinding(PostgreSqlBuilder.PostgreSqlPort, assignRandomHostPort: true)
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilMessageIsLogged(ContainerStartedLog)
                .UntilPortIsAvailable(5432));

        return postgreSqlBuilder.Build();
    }

    private ServiceProvider BuildServiceProvider()
    {
        var databaseSettings = new DatabaseSettings()
        {
            ConnectionString = TestModule.Current.GetDbConnectionString()
        };

        var testApplicationBuilder = WebApplication.CreateBuilder();
        testApplicationBuilder.Services.AddDatabase(databaseSettings);
        return testApplicationBuilder.Services.BuildServiceProvider();
    }
}
