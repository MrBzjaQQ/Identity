using Identity.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Users.Application.Services.Users.Port;
public interface IUserManagerProxy
{
    /// <summary>
    /// Creates user with specified password
    /// </summary>
    Task<IdentityResult> CreateAsync(User user, string password);

    /// <summary>
    /// Checks if user exists
    /// </summary>
    Task<bool> IsUserExists(string email);

    /// <summary>
    /// Gets user by specified email
    /// </summary>
    Task<User?> FindByEmailAsync(string email);

    /// <summary>
    /// Finds user by it's ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User entity</returns>
    Task<User?> FindByIdAsync(string id);
}
