using Testcontainers.PostgreSql;

namespace Identity.Application.Tests;
public class TestModule
{
    private static TestModule? _current;
    private PostgreSqlContainer? _dbContainer;
    private const string DbContainerIsNotCreated = "PostgreSQL container is not created";

    public static TestModule Current = _current ??= new TestModule();

    public void SetDbContainer(PostgreSqlContainer container)
    {
        _dbContainer = container;
    }

    public async Task StartDbContainerAsync()
    {
        if (_dbContainer == null)
        {
            throw new InvalidOperationException(DbContainerIsNotCreated);
        }

        await _dbContainer.StartAsync();
    }

    public async Task StopDbContainerAsync()
    {
        if (_dbContainer == null)
        {
            throw new InvalidOperationException(DbContainerIsNotCreated);
        }

        await _dbContainer.StopAsync();
    }

    public string GetDbConnectionString()
    {
        if (_dbContainer == null)
        {
            throw new InvalidOperationException(DbContainerIsNotCreated);
        }

        return _dbContainer.GetConnectionString();
    }
}
