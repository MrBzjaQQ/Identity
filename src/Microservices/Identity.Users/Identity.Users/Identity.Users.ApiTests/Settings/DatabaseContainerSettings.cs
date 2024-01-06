namespace Identity.Users.ApiTests.Settings;
public sealed record DatabaseContainerSettings
{
    /// <summary>
    /// Container host
    /// </summary>
    public string Host { get; init; } = string.Empty;

    /// <summary>
    /// Name of the tests database
    /// </summary>
    public string DatabaseName { get; init; } = string.Empty;

    /// <summary>
    /// Name of user
    /// </summary>
    public string Username { get; init; } = string.Empty;

    /// <summary>
    /// Database user password
    /// </summary>
    public string Password { get; init; } = string.Empty;
}
