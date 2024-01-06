using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Users.Application.Services.Users.Mappers.Users;
public static class CreateUserMappings
{
    public static User ToDomainUser(this CreateUserRequest request)
    {
        return new User()
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };
    }

    public static CreateUserResponse ToSchemaResponse(this IdentityResult result, string? userId)
    {
        return new CreateUserResponse
        {
            UserId = result.Succeeded ? userId : null,
            Errors = result.Errors.Select(error => error.ToSchemaError()).ToList(),
            IsSucceeded = result.Succeeded
        };
    }

    public static IdentityErrorResponse ToSchemaError(this IdentityError error)
    {
        return new IdentityErrorResponse()
        {
            Code = error.Code,
            Description = error.Description
        };
    }
}
