using Identity.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Users.Application.Services;
public interface IIdentityDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<IdentityRole> Roles { get; }
    public DbSet<IdentityRoleClaim<string>> RoleClaims { get; }
    public DbSet<IdentityUserRole<string>> UserRoles { get; }
}
