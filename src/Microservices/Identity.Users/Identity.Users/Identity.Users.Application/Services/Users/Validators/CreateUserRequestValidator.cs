using FluentValidation;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Resources;

namespace Identity.Users.Application.Services.Users.Validators;
public sealed class CreateUserRequestValidator: AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(request => request.Password)
            .Equal(request => request.PasswordConfirm)
            .WithMessage(ErrorMessages.PasswordsShouldMatch);

        RuleFor(request => request.Password)
            .NotNull()
            .WithMessage(ErrorMessages.PasswordCannotBeEmpty)
            .NotEmpty()
            .WithMessage(ErrorMessages.PasswordCannotBeEmpty);

        RuleFor(request => request.Email)
            // regular expression from docs https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage(ErrorMessages.IncorrectEmail);

        RuleFor(request => request.UserName)
            .NotNull()
            .WithMessage(ErrorMessages.UserNameCannotBeEmpty)
            .NotEmpty()
            .WithMessage(ErrorMessages.UserNameCannotBeEmpty);
    }
}
