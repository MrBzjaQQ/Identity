using FluentValidation;
using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Resources;

namespace Identity.Users.Application.Services.Users.Validators;
public sealed class CreateUserRequestValidator: AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator(IUserManagerProxy userManagerProxy)
    {
        RuleFor(request => request.Password)
            .Equal(request => request.PasswordConfirm)
            .WithMessage(ErrorMessages.PasswordsShouldMatch)
            .NotEmpty()
            .WithMessage(ErrorMessages.PasswordCannotBeEmpty);

        RuleFor(request => request.Email)
            // regular expression from docs https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage(ErrorMessages.IncorrectEmail)
            .MustAsync(async (email, cancellationToken) => !await userManagerProxy.IsUserExists(email))
            .WithMessage(ErrorMessages.UserWithEmailAlreadyExists);

        RuleFor(request => request.UserName)
            .NotEmpty()
            .WithMessage(ErrorMessages.UserNameCannotBeEmpty);
    }
}
