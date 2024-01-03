using FluentAssertions;
using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Tests.Services.Users;
public class CreateUserTests: TestFixtureBase
{
    private IUsersService _usersService;

    [OneTimeSetUp]
    public override void SetUp()
    {
        base.SetUp();
        _usersService = ServiceProvider.GetRequiredService<IUsersService>();
    }

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
        var result = await _usersService.CreateUserAsync(request);

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

    //[Test]
    //public async Task CreateUser__PasswordsNotMatch__ShouldThrowBadRequest()
    //{
    //    // Arrange
    //    var request = new CreateUserRequest
    //    {
    //        Email = "test@example.com",
    //        Password = "Pa55w0rd!",
    //        PasswordConfirm = "Pa55w0rd!!",
    //        PhoneNumber = "+1234567890",
    //        UserName = "TestUser1"
    //    };

    //    var shouldThrow = () => _usersService.CreateUserAsync(request);

    //    // Act & Assert
    //    await shouldThrow.Should().ThrowAsync<>();
    //}
}
