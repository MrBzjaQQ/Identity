using Identity.Users.Application.Services.Users.Mappers.Users;
using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Resources;

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
        if (await _userManagerProxy.IsUserExists(request.Email))
        {
            throw new HttpRequestException(
                ErrorMessages.UserWithEmailAlreadyExists,
                null,
                System.Net.HttpStatusCode.BadRequest);
        }

        var user = request.ToDomainUser();
        var result = await _userManagerProxy.CreateAsync(user, request.Password);
        return result.ToSchemaResponse(user.Id);
    }
}
