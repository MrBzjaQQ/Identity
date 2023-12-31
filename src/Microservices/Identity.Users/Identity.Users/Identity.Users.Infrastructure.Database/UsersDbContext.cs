﻿using Identity.Users.Application.Services;
using Identity.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Users.Infrastructure.Database;
public sealed class UsersDbContext: IdentityDbContext<User>, IUsersDbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options)
    {
        
    }
}
