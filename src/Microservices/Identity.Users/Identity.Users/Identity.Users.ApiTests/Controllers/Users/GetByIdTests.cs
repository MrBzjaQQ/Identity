using FluentAssertions;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Application.Services.Users.Port.Contract.GetUserDetails;
using Identity.Users.Resources;
using NUnit.Framework;
using Refit;

namespace Identity.Users.ApiTests.Controllers.Users;
public class GetByIdTests: TestFixtureBase
{
    [Test]
    public async Task GetById_UserExists_ReturnsResult()
    {
        // Arrange
        var createRequest = new CreateUserRequest
        {
            Email = "test@example.com",
            Password = "Pa55w0rd!",
            PasswordConfirm = "Pa55w0rd!",
            PhoneNumber = "+1234567890",
            UserName = "TestUser1"
        };

        var createResult = await UsersService.CreateUserAsync(createRequest);
        createResult.UserId.Should().NotBeNull();
        var userId = createResult.UserId!;

        var userDetails = new GetUserDetailsResponse()
        {
            Id = userId,
            Email = createRequest.Email,
            IsEmailConfirmed = false,
            IsLockoutEnabled = true,
            LockoutEnd = null,
            IsPhoneNumberConfirmed = false,
            IsTwoFactorEnabled = false,
            PhoneNumber = createRequest.PhoneNumber,
            UserName = createRequest.UserName
        };

        // Act
        var getResult = await UsersService.GetByIdAsync(userId);

        // Assert
        getResult.Should().BeEquivalentTo(userDetails);
    }

    [Test]
    public async Task GetById_UserNotFound_ThrowsException()
    {
        // Arrange
        string defaultUserId = Guid.NewGuid().ToString();
        var shouldThrow = () => UsersService.GetByIdAsync(defaultUserId);

        // Act & Assert
        var exceptionAssert = await shouldThrow.Should().ThrowAsync<ApiException>();
        exceptionAssert.Which.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
