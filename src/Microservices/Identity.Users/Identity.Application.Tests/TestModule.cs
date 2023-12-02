using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Tests;
public class TestModule
{
    public IServiceProvider ServiceProvider { get; }
    private static TestModule? _current;
    public static TestModule Current = _current ??= new TestModule();

    public TestModule()
    {
        var builder = WebApplication
            .CreateBuilder()
            .BuildTestApplication();

        ServiceProvider = builder.Build().Services;
    }

    public async Task RunInScopeAsync(Func<IServiceProvider, Task> action)
    {
        await using var scope = ServiceProvider.CreateAsyncScope();
        await action(scope.ServiceProvider);
    }

    public void RunInScope(Action<IServiceProvider> action)
    {
        using var scope = ServiceProvider.CreateScope();
        action(scope.ServiceProvider);
    }
}
