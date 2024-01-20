using Identity.Users.Application.Services.Users.Port.Contract.GetUserDetails;
using Identity.Users.Domain.Entities;

namespace Identity.Users.Application.Services.Users.Mappers.Users;
public static class GetUserDetailsMappings
{
    public static GetUserDetailsResponse ToSchemaDetails(this User user)
    {
        return new GetUserDetailsResponse()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            IsTwoFactorEnabled = user.TwoFactorEnabled,
            IsLockoutEnabled = user.LockoutEnabled,
            IsEmailConfirmed = user.EmailConfirmed,
            IsPhoneNumberConfirmed = user.PhoneNumberConfirmed,
            LockoutEnd = user.LockoutEnd
        };
    }
}
