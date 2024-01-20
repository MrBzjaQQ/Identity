using FluentValidation;
using Identity.Users.Application.Services.Users.Port.Contract.GetList;

namespace Identity.Users.Application.Services.Users.Validators;
public class GetUsersListRequestValidator: AbstractValidator<GetUsersListRequest>
{
    public GetUsersListRequestValidator()
    {
        RuleFor(request => request.Take)
            .GreaterThanOrEqualTo(0);

        RuleFor(request => request.Skip)
            .GreaterThanOrEqualTo(0);
    }
}
