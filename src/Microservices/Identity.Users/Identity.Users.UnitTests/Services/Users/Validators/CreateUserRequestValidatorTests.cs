using FluentAssertions;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Resources;
using Identity.Users.Application.Services.Users.Validators;

namespace Identity.Users.UnitTests.Services.Users.Validators;
public class CreateUserRequestValidatorTests
{
    private readonly CreateUserRequestValidator _validator = new CreateUserRequestValidator();

    [Test]
    public void CreateUser__PasswordsNotMatch__ShouldThrowBadRequest()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Email = "test@example.com",
            Password = "Pa55w0rd!",
            PasswordConfirm = "Pa55w0rd!!",
            PhoneNumber = "+1234567890",
            UserName = "TestUser1"
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().BeEquivalentTo(new[] { 
            new
            {
                ErrorMessage = ErrorMessages.PasswordsShouldMatch
            }
        });
    }

    [Test]
    public void CreateUser__EmailIncorrect__ShouldThrowBadRequest()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Email = string.Empty,
            Password = "Pa55w0rd!",
            PasswordConfirm = "Pa55w0rd!",
            PhoneNumber = "+1234567890",
            UserName = "TestUser1"
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().BeEquivalentTo(new[] {
            new
            {
                ErrorMessage = ErrorMessages.IncorrectEmail
            }
        });
    }

    [Test]
    public void CreateUser__UserNameEmpty__ShouldThrowBadRequest()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Email = "test@example.com",
            Password = "Pa55w0rd!",
            PasswordConfirm = "Pa55w0rd!",
            PhoneNumber = "+1234567890"
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().BeEquivalentTo(new[] {
            new
            {
                ErrorMessage = ErrorMessages.UserNameCannotBeEmpty
            }
        });
    }
}
