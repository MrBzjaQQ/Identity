using Identity.Users.Application.Services;
using Identity.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Tests;
public abstract class TestFixtureBase
{
    private IServiceScope _testRunScope = null!;
    protected IServiceProvider ServiceProvider => _testRunScope.ServiceProvider;
    protected IUsersDbContext UsersDbContext => ServiceProvider.GetRequiredService<IUsersDbContext>();

    [SetUp]
    public virtual void SetUp()
    {
        _testRunScope = TestModule.Current.ServiceProvider.CreateScope();

        // Reset data
        ClearDatabase();
    }

    [TearDown]
    public void TearDown()
    {
        _testRunScope.Dispose();
    }

    private void ClearDatabase()
    {
        var db = ServiceProvider.GetRequiredService<UsersDbContext>();
        var tableNames = db.Model.GetEntityTypes()
            .Select(et => et.GetTableName())
            .Distinct();

        foreach (var tableName in tableNames)
        {
#pragma warning disable EF1002 // SQL Injection is not possible due to project of application tests
            db.Database.ExecuteSqlRaw($"DELETE FROM \"{tableName}\";");
#pragma warning restore EF1002 // SQL Injection is not possible due to project of application tests
        }
    }

    protected void NewScope()
    {
        _testRunScope.Dispose();
        _testRunScope = TestModule.Current.ServiceProvider.CreateScope();
    }
}
