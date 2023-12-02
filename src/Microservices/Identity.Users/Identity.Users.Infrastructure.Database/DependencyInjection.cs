using Identity.Users.Application.Services;
using Identity.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Identity.Users.Infrastructure.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, DatabaseSettings? databaseSettings)
    {
        if (string.IsNullOrWhiteSpace(databaseSettings?.ConnectionString))
        {
            throw new ArgumentNullException(nameof(databaseSettings.ConnectionString));
        }

        serviceCollection.AddDbContext<UsersDbContext>(options => options.UseNpgsql(databaseSettings.ConnectionString));
        serviceCollection.AddScoped<IUsersDbContext>(provider => provider.GetRequiredService<UsersDbContext>());
        serviceCollection.AddTransient<IUsersDbMigrator, UsersDbMigrator>();
        serviceCollection.AddIdentityCore<User>(options => {
            options.SignIn.RequireConfirmedAccount = true;
        }).AddEntityFrameworkStores<UsersDbContext>();

        // TODO - move to appsettings (?)
        serviceCollection.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        return serviceCollection;
    }

    public static async Task<IHost> MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var identityDbMigrator = scope.ServiceProvider.GetRequiredService<IUsersDbMigrator>();
        await identityDbMigrator.MigrateAsync();
        await identityDbMigrator.SeedDataAsync();
        return host;
    }
}