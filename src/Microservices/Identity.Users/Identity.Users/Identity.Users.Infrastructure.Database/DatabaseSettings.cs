namespace Identity.Users.Infrastructure.Database;
public sealed record DatabaseSettings
{
    public string? ConnectionString { get; set; }
}
