using Identity.Users.Application.Services.Users.Mappers.Users;
using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;

namespace Identity.Users.Application.Services.Users;
public sealed class UsersService: IUsersService
{
    private readonly IUserManagerProxy _userManagerProxy;

    public UsersService(IUserManagerProxy userManagerProxy)
    {
        _userManagerProxy = userManagerProxy;
    }

    public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
    {
        var user = request.ToDomainUser();
        var result = await _userManagerProxy.CreateAsync(user, request.Password);
        return result.ToSchemaResponse(user.Id);
    }
}
