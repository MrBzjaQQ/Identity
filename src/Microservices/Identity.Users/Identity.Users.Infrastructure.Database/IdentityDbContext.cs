using Identity.Users.Application.Services;
using Identity.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Users.Infrastructure.Database;
public sealed class IdentityDbContext: IdentityDbContext<User>, IIdentityDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
        : base(options)
    {
        
    }
}
