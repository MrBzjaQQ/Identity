using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using Identity.Users.ApiTests.Services;
using Identity.Users.ApiTests.Settings;
using Identity.Users.Resources.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net;
using Testcontainers.PostgreSql;

namespace Identity.Users.ApiTests;
public abstract class TestFixtureBase
{
    private const string SettingsAreNull = "Database container settings are NULL";
    private const string ContainerStartedLog = "PostgreSQL init process complete; ready for start up.";
    private const string DbContainerName = "postgres-testcontainer";
    private const string ApplicationImageName = "identityusers";
    private const string ApplicationContainerName = "identityusersapp";
    private const string DockerfileDirectory = "..\\";
    private const string DockerfileName = "ApiTests.Dockerfile";

    private PostgreSqlContainer _dbContainer;
    private IFutureDockerImage _applicationImage;
    private IContainer _appContainer;
    private ServiceProvider _serviceProvider;
    private IUsersEndpointsService _usersService;

    protected IUsersEndpointsService UsersService => _usersService;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _applicationImage = new ImageFromDockerfileBuilder()
            .WithName(ApplicationImageName)
            .WithDockerfileDirectory(CommonDirectoryPath.GetBinDirectory(), DockerfileDirectory)
            .WithDockerfile(DockerfileName)
            .Build();

        await _applicationImage.CreateAsync().ConfigureAwait(false);
    }

    [SetUp]
    public async Task SetUp()
    {
        var appSettings = GetApplicationSettings();
        _dbContainer = CreateDbContainer(appSettings?.DbContainer);
        _appContainer = CreateApplicationContainer(_dbContainer, _applicationImage);

        await _dbContainer.StartAsync().ConfigureAwait(false);
        await _appContainer.StartAsync().ConfigureAwait(false);

        _serviceProvider = BuildServiceProvider(
            hostname: _appContainer.Hostname,
            port: _appContainer.GetMappedPublicPort(8081),
            scheme: Uri.UriSchemeHttps);
        _usersService = _serviceProvider.GetRequiredService<IUsersEndpointsService>();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _serviceProvider.DisposeAsync();

        await _appContainer.StopAsync().ConfigureAwait(false);
        await _dbContainer.StopAsync().ConfigureAwait(false);

        await _appContainer.DisposeAsync();
        await _dbContainer.DisposeAsync();
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _applicationImage.DeleteAsync().ConfigureAwait(false);
        await _applicationImage.DisposeAsync();
    }

    private static TestApplicationSettings? GetApplicationSettings()
    {
        var tempBuilder = new ConfigurationBuilder();
        tempBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
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
            .WithPortBinding(PostgreSqlBuilder.PostgreSqlPort, assignRandomHostPort: false)
            .WithName(DbContainerName)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(ContainerStartedLog));

        return postgreSqlBuilder.Build();
    }

    private static IContainer CreateApplicationContainer(PostgreSqlContainer dbContainer, IFutureDockerImage image)
    {
        var containerBuilder = new ContainerBuilder()
            .WithImage(image)
            .DependsOn(dbContainer)
            .WithWaitStrategy(Wait
                .ForUnixContainer()
                .UntilMessageIsLogged(ApplicationStartup.ApplicationStarted)
                .UntilPortIsAvailable(8080)
                .UntilPortIsAvailable(8081))
            .WithPortBinding(8080, 8080)
            .WithPortBinding(8081, 8081)
            .WithName(ApplicationContainerName)
            .WithEnvironment("Kestrel__Certificates__Default__Path", "/usr/local/share/ca-certificates/apitests_CA.pfx")
            .WithEnvironment("Kestrel__Certificates__Default__Password", "Pa55w0rd!");

        return containerBuilder.Build();
    }

    private static ServiceProvider BuildServiceProvider(string hostname, int port, string scheme)
    {
        var applicationEndpoint = new UriBuilder()
        {
            Host = hostname,
            Port = port,
            Scheme = scheme,
        }.Uri.ToString();

        var testApplicationBuilder = WebApplication
            .CreateBuilder()
            .BuildTestApplication(applicationEndpoint);

        return testApplicationBuilder.Services.BuildServiceProvider();
    }
}
