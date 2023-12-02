using Identity.Users.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Tests;

[SetUpFixture]
public class SetUpTests
{
    [OneTimeSetUp]
    public async Task RunBeforeTests()
    {
        await TestModule.Current.RunInScopeAsync(async (serviceProvider) =>
        {
            var dbMigrator = serviceProvider.GetRequiredService<IUsersDbMigrator>();
            await dbMigrator.MigrateAsync();
            await dbMigrator.SeedDataAsync();
        });
    }

    [OneTimeTearDown]
    public async Task RunAfterTests()
    {
        await TestModule.Current.RunInScopeAsync(async (serviceProvider) =>
        {
            var dbMigrator = serviceProvider.GetRequiredService<IUsersDbMigrator>();
            await dbMigrator.EnsureDatabaseDeletedAsync();
        });
    }
}
