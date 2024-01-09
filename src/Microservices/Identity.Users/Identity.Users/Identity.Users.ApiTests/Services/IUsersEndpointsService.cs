using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Application.Services.Users.Port.Contract.GetUserDetails;
using Refit;

namespace Identity.Users.ApiTests.Services;
public interface IUsersEndpointsService
{
    [Post("/api/Users/createUser")]
    Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default);

    [Get("/api/Users/getById/{id}")]
    Task<GetUserDetailsResponse> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
