using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;

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

    public async Task<bool> IsUserExists(string email)
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
}
