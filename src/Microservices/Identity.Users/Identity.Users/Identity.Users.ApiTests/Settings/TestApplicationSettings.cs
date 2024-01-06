namespace Identity.Users.ApiTests.Settings;
public sealed record TestApplicationSettings
{
    /// <summary>
    /// Database container settings
    /// </summary>
    public DatabaseContainerSettings? DbContainer { get; init; }
}
