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

    /// <summary>
    /// Gets users list
    /// </summary>
    /// <param name="take">Amount to take</param>
    /// <param name="skip">Amount to skip</param>
    /// <param name="cancellationToken">Operation cancellation token</param>
    Task<IList<UserListItem>> GetList(
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
    Task<IList<UserListItem>> GetList(
        AdHocSpecification<User> filter,
        int take = int.MaxValue,
        int skip = 0,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts users
    /// </summary>
    /// <param name="cancellationToken">Operation cancellation token</param>
    Task<long> Count(CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts users by filter
    /// </summary>
    /// <param name="filter">Filtering expression</param>
    /// <param name="cancellationToken">Operation cancellation token</param>
    Task<long> Count(
        AdHocSpecification<User> filter,
        CancellationToken cancellationToken = default);
}
