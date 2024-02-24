using Identity.Users.Application.Services.Shared.Port.Contact;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Application.Services.Users.Port.Contract.GetList;
using Identity.Users.Application.Services.Users.Port.Contract.GetUserDetails;

namespace Identity.Users.Application.Services.Users.Port;
public interface IUsersService
{
    /// <summary>
    /// Creates user
    /// </summary>
    /// <param name="request">Data for user creation</param>
    /// <returns>Result of user creation</returns>
    Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);

    /// <summary>
    /// Gets user by it's ID
    /// </summary>
    /// <param name="id">User identifier</param>
    /// <returns>User info</returns>
    Task<GetUserDetailsResponse> GetByIdAsync(string id);

    /// <summary>
    /// Gets list of users
    /// </summary>
    /// <param name="request">Users list filter</param>
    /// <returns>List of users</returns>
    Task<PagedListResponse<UserListItem>> GetListAsync(GetUsersListRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes user
    /// </summary>
    /// <param name="id">User Id</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteUserAsync(string id, CancellationToken cancellationToken = default);
}
