using Identity.Users.Infrastructure.Database;

namespace Identity.Application.Tests.Settings;
public sealed record TestApplicationSettings
{
    /// <summary>
    /// Settings for database connection etc
    /// </summary>
    public DatabaseSettings? Database { get; set; }
}
