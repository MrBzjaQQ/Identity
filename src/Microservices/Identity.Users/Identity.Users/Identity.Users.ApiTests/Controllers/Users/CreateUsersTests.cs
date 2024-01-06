using FluentAssertions;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using NUnit.Framework;

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
        var response = await UsersService.CreateUser(request);

        // Assert
        response.Should().BeEquivalentTo(new CreateUserResponse()
        {
            Errors = Array.Empty<IdentityErrorResponse>(),
            IsSucceeded = true,
        }, config => config.Excluding(response => response.UserId));

        response.UserId.Should().NotBeNull().And.NotBeEmpty();
    }
}
