using Identity.Users.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Identity.Users.Infrastructure.Database;
public sealed class UsersDbMigrator: IUsersDbMigrator
{
    private readonly UsersDbContext _context;

    public UsersDbMigrator(UsersDbContext context)
    {
        _context = context;
    }

    public async Task MigrateAsync()
    {
        await _context.Database.MigrateAsync();
    }

    public Task SeedDataAsync()
    {
        // TODO - Seed Data
        return Task.CompletedTask;
    }

    public async Task EnsureDatabaseDeletedAsync()
    {
        await _context.Database.EnsureDeletedAsync();
    }
}
