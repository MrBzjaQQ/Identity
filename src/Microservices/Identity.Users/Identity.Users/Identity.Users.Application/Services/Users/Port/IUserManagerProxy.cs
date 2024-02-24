using Identity.Users.Application.Services.Users.Port.Contract.GetList;
using Identity.Users.Domain.Entities;
using LinqSpecs;
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
    Task<bool> IsUserExistsAsync(string email);

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

    /// <summary>
    /// Gets users list
    /// </summary>
    /// <param name="take">Amount to take</param>
    /// <param name="skip">Amount to skip</param>
    /// <param name="cancellationToken">Operation cancellation token</param>
    Task<IList<UserListItem>> GetListAsync(
        int take = int.MaxValue,
        int skip = 0,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets users list
    /// </summary>
    /// <param name="filter">Filtering expression</param>
    /// <param name="take">Amount to take</param>
    /// <param name="skip">Amount to skip</param>
    /// <param name="cancellationToken">Operation cancellation token</param>
    Task<IList<UserListItem>> GetListAsync(
        AdHocSpecification<User> filter,
        int take = int.MaxValue,
        int skip = 0,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts users
    /// </summary>
    /// <param name="cancellationToken">Operation cancellation token</param>
    Task<long> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts users by filter
    /// </summary>
    /// <param name="filter">Filtering expression</param>
    /// <param name="cancellationToken">Operation cancellation token</param>
    Task<long> CountAsync(
        AdHocSpecification<User> filter,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes user
    /// </summary>
    /// <param name="user">User</param>
    Task DeleteAsync(User user);
}
