using Identity.Users.Application.Services;
using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Tests;
public abstract class TestFixtureBase
{
    private IServiceScope? _testRunScope = null;
    private ServiceProvider _serviceProvider = null!;
    protected IServiceProvider ServiceProvider => _serviceProvider;
    protected IUsersDbContext UsersDbContext => ServiceProvider.GetRequiredService<IUsersDbContext>();
    protected IUsersService UsersService => ServiceProvider.GetRequiredService<IUsersService>();

    [SetUp]
    public virtual async Task SetUp()
    {
        _serviceProvider = BuildServiceProvider();
        _testRunScope = _serviceProvider.CreateScope();
        await ClearDatabase();
    }

    [TearDown]
    public async Task TearDown()
    {
        _testRunScope?.Dispose();
        _testRunScope = null;
        await _serviceProvider.DisposeAsync();
        _serviceProvider = null!;
    }


    protected virtual void NewScope()
    {
        _serviceProvider.DisposeAsync().AsTask().GetAwaiter().GetResult();
        _serviceProvider = BuildServiceProvider();
    }

    protected ServiceProvider BuildServiceProvider()
    {
        var databaseSettings = new DatabaseSettings()
        {
            ConnectionString = TestModule.Current.GetDbConnectionString()
        };

        var testApplicationBuilder = WebApplication
            .CreateBuilder()
            .BuildTestApplication(databaseSettings);

        return testApplicationBuilder.Services.BuildServiceProvider();
    }

    private async Task ClearDatabase()
    {
        var db = ServiceProvider.GetRequiredService<UsersDbContext>();
        var tableNames = db.Model.GetEntityTypes()
            .Select(et => et.GetTableName())
            .Distinct();

        foreach (var tableName in tableNames)
        {
#pragma warning disable EF1002 // SQL Injection is not possible due to project of application tests
            await db.Database.ExecuteSqlRawAsync($"DELETE FROM \"{tableName}\";");
#pragma warning restore EF1002 // SQL Injection is not possible due to project of application tests
        }
    }
}
