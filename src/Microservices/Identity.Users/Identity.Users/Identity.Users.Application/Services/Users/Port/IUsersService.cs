using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;

namespace Identity.Users.Application.Services.Users.Port;
public interface IUsersService
{
    Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);
}
