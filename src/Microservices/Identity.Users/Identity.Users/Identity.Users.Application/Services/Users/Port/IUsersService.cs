using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
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
}
