using Identity.Users.Application.Exceptions;
using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Port.Contract.GetList;
using Identity.Users.Domain.Entities;
using LinqSpecs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Users.Application.Services.Users;
public sealed class UserManagerProxy: IUserManagerProxy
{
    private readonly UserManager<User> _userManager;

    public UserManagerProxy(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> CreateAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<bool> IsUserExistsAsync(string email)
    {
        var result = await _userManager.FindByEmailAsync(email);
        return result is not null;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> FindByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<IList<UserListItem>> GetListAsync(
        int take = int.MaxValue,
        int skip = 0,
        CancellationToken cancellationToken = default)
    {
        return await GetList(_userManager.Users, take, skip, cancellationToken);
    }

    public async Task<IList<UserListItem>> GetListAsync(
        AdHocSpecification<User> filter,
        int take = int.MaxValue,
        int skip = 0,
        CancellationToken cancellationToken = default)
    {
        var query =  _userManager.Users.Where(filter);
        return await GetList(query, take, skip, cancellationToken);
    }

    private async Task<IList<UserListItem>> GetList(
        IQueryable<User> usersQuery,
        int take,
        int skip,
        CancellationToken cancellationToken)
    {
        return await usersQuery
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .OrderBy(user => user.Id)
            .Select(x => new UserListItem
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName
            }).ToListAsync(cancellationToken);
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _userManager.Users.CountAsync(cancellationToken);
    }

    public async Task<long> CountAsync(
        AdHocSpecification<User> filter,
        CancellationToken cancellationToken = default)
    {
        return await _userManager.Users
            .Where(filter)
            .CountAsync(cancellationToken);
    }

    public async Task DeleteAsync(User user)
    {
        await _userManager.DeleteAsync(user);
    }
}
