using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Refit;

namespace Identity.Users.ApiTests.Services;
public interface IUsersEndpointsService
{
    [Post("/api/Users/createUser")]
    Task<CreateUserResponse> CreateUser(CreateUserRequest request, CancellationToken cancellationToken = default);
}
