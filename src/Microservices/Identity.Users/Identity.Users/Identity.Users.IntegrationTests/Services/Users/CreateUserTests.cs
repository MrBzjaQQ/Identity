using FluentAssertions;
using Identity.Users.Application.Exceptions;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Resources;
using Microsoft.EntityFrameworkCore;

namespace Identity.Users.IntegrationTests.Services.Users;
public class CreateUserTests : TestFixtureBase
{
    [Test]
    public async Task ShouldCreateUser()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Email = "test@example.com",
            Password = "Pa55w0rd!",
            PasswordConfirm = "Pa55w0rd!",
            PhoneNumber = "+1234567890",
            UserName = "TestUser1"
        };

        // Act
        var result = await UsersService.CreateUserAsync(request);

        // Assert
        NewScope();
        result.IsSucceeded.Should().BeTrue();

        var user = await UsersDbContext.Users.FirstOrDefaultAsync();
        user.Should().NotBeNull();
        user!.PhoneNumber.Should().Be(request.PhoneNumber);
        user!.Email.Should().Be(request.Email);
        user!.UserName.Should().Be(request.UserName);
        user!.PasswordHash.Should().NotBeEmpty();
    }

    [Test]
    public async Task CreateUser__UserAlreadyExists__ShouldThrowBadRequest()
    {
        // Arrange
        var request = new CreateUserRequest
        {
            Email = "test@example.com",
            Password = "Pa55w0rd!",
            PasswordConfirm = "Pa55w0rd!",
            PhoneNumber = "+1234567890",
            UserName = "TestUser1"
        };

        await UsersService.CreateUserAsync(request);
        var shouldThrow = () => UsersService.CreateUserAsync(request);

        // Act & Assert
        var exceptionAssert = await shouldThrow.Should().ThrowAsync<BadRequestException>();
        exceptionAssert.WithMessage(ErrorMessages.UserWithEmailAlreadyExists);
    }
}
