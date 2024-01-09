namespace Identity.Users.IntegrationTests.Settings;
public sealed record TestApplicationSettings
{
    /// <summary>
    /// Settings for database connection etc
    /// </summary>
    public DatabaseContainerSettings? DbContainer { get; set; }
}
