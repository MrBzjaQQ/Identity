using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users;
using Moq;
using Identity.Users.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using FluentAssertions;
using System.Linq.Expressions;

namespace Identity.Users.UnitTests.Services.Users.UsersServiceTests;
public class CreateUsersTests
{
    private const string Password = "Pa55w0rd!";
    private User _defaultUser = new User()
    {
        UserName = "testUser1",
        Email = "test@example.com",
        PhoneNumber = "+12345678900"
    };
    private Mock<IUserManagerProxy> _userManagerProxyMock;
    private UsersService _usersService;

    [SetUp]
    public void SetUp()
    {
        _userManagerProxyMock = new Mock<IUserManagerProxy>();
        _usersService = new UsersService(_userManagerProxyMock.Object);
    }

    [Test]
    public async Task CreateUser_CorrectRequest_ReturnsOk()
    {
        // Arrange
        Expression<Func<IUserManagerProxy, Task<IdentityResult>>> createExpression = mock => mock.CreateAsync(It.IsAny<User>(), Password);
        var request = new CreateUserRequest
        {
            UserName = _defaultUser.UserName ?? string.Empty,
            Email = _defaultUser.Email ?? string.Empty,
            Password = Password,
            PasswordConfirm = Password
        };

        _userManagerProxyMock.Setup(createExpression)
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _usersService.CreateUserAsync(request);

        // Assert
        result.IsSucceeded.Should().BeTrue();
        result.Errors.Should().BeEmpty();

        _userManagerProxyMock.Verify(createExpression, Times.Once);
    }

    [Test]
    public async Task CreateUser_CorrectRequest_ReturnsErrors()
    {
        // Arrange
        Expression<Func<IUserManagerProxy, Task<IdentityResult>>> createExpression = mock => mock.CreateAsync(It.IsAny<User>(), Password);
        var request = new CreateUserRequest
        {
            UserName = _defaultUser.UserName ?? string.Empty,
            Email = _defaultUser.Email ?? string.Empty,
            Password = Password,
            PasswordConfirm = Password
        };

        var error = new IdentityError() { Code = "ERR", Description = "Some Error" };
        _userManagerProxyMock.Setup(createExpression)
            .ReturnsAsync(IdentityResult.Failed(error));

        // Act
        var result = await _usersService.CreateUserAsync(request);

        // Assert
        result.IsSucceeded.Should().BeFalse();
        result.Errors.Should().BeEquivalentTo(new[]
        {
            new IdentityErrorResponse()
            {
                Code = error.Code,
                Description = error.Description
            }
        });

        _userManagerProxyMock.Verify(createExpression, Times.Once);
    }
}
