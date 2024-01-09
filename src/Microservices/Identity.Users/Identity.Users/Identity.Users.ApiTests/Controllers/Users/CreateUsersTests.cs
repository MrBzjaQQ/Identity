using FluentAssertions;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using NUnit.Framework;
using Refit;

namespace Identity.Users.ApiTests.Controllers.Users;

public class CreateUsersTests: TestFixtureBase
{
    [Test]
    public async Task ShouldCreateUser()
    {
        // Arrange
        var request = new CreateUserRequest()
        {
            Email = "test@example.com",
            Password = "Pa55w0rd!",
            PasswordConfirm = "Pa55w0rd!",
            PhoneNumber = "+12345678900",
            UserName = "ApiTestUser"
        };

        // Act
        var response = await UsersService.CreateUserAsync(request);

        // Assert
        response.Should().BeEquivalentTo(new CreateUserResponse()
        {
            Errors = Array.Empty<IdentityErrorResponse>(),
            IsSucceeded = true,
        }, config => config.Excluding(response => response.UserId));

        response.UserId.Should().NotBeNull().And.NotBeEmpty();
    }

    [Test]
    public async Task ShouldThrowIfRequestInvalid()
    {
        // Arrange
        var request = new CreateUserRequest();
        var shouldThrow = () => UsersService.CreateUserAsync(request);

        // Act & Assert
        await shouldThrow.Should().ThrowAsync<ValidationApiException>();
    }
}
