namespace Identity.Users.Application.Services;
public interface IUsersDbMigrator
{
    Task MigrateAsync();
    Task SeedDataAsync();
    Task EnsureDatabaseDeletedAsync();
}
