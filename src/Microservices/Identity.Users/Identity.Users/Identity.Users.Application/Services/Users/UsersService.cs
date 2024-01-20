using Identity.Users.Application.Exceptions;
using Identity.Users.Application.Services.Shared.Port.Contact;
using Identity.Users.Application.Services.Users.Mappers.Users;
using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Application.Services.Users.Port.Contract.GetList;
using Identity.Users.Application.Services.Users.Port.Contract.GetUserDetails;
using Identity.Users.Domain.Entities;
using Identity.Users.Resources;
using LinqSpecs;

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
            throw new BadRequestException(ErrorMessages.UserWithEmailAlreadyExists);
        }

        var user = request.ToDomainUser();
        var result = await _userManagerProxy.CreateAsync(user, request.Password);
        return result.ToSchemaResponse(user.Id);
    }

    public async Task<GetUserDetailsResponse> GetByIdAsync(string id)
    {
        var user = await _userManagerProxy.FindByIdAsync(id);
        EntityNotFoundException.ThrowIfNull(user, string.Format(ErrorMessages.UserNotFoundTemplate, id));
        return user.ToSchemaDetails();
    }

    public async Task<PagedListResponse<UserListItem>> GetList(GetUsersListRequest request, CancellationToken cancellationToken)
    {
        var filter = new AdHocSpecification<User>(f => f.LockoutEnd.HasValue);
        var users = await _userManagerProxy.GetList(
            take: request.Take,
            skip: request.Skip,
            cancellationToken);

        return new()
        {
            TotalCount = await _userManagerProxy.Count(cancellationToken),
            Items = users
        };
    }
}
