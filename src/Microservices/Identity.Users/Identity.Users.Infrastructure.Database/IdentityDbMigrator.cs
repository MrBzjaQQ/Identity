using Identity.Users.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Identity.Users.Infrastructure.Database;
public sealed class IdentityDbMigrator: IIdentityDbMigrator
{
    private readonly IdentityDbContext _context;

    public IdentityDbMigrator(IdentityDbContext context)
    {
        _context = context;
    }

    public async Task MigrateAsync()
    {
        await _context.Database.MigrateAsync();
    }

    public async Task SeedDataAsync()
    {
        // TODO - Seed Data
    }

    public async Task EnsureDatabaseDeletedAsync()
    {
        await _context.Database.EnsureDeletedAsync();
    }
}
