namespace Identity.Users.Application.Services;
public interface IIdentityDbMigrator
{
    Task MigrateAsync();
    Task SeedDataAsync();
    Task EnsureDatabaseDeletedAsync();
}
