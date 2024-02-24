using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users;
using Moq;
using System.Linq.Expressions;
using Identity.Users.Domain.Entities;
using FluentAssertions;
using Identity.Users.Application.Exceptions;

namespace Identity.Users.UnitTests.Services.Users.UsersServiceTests;
public class DeleteUserTests
{
    private string DefaultUserId = Guid.NewGuid().ToString();
    private Expression<Func<IUserManagerProxy, Task<User?>>> _getUserExpression;
    private Expression<Func<IUserManagerProxy, Task>> _deleteUserExpression;
    private Mock<IUserManagerProxy> _userManagerProxyMock;
    private UsersService _usersService;

    [SetUp]
    public void SetUp()
    {
        _getUserExpression = proxy => proxy.FindByIdAsync(DefaultUserId);
        _deleteUserExpression = x => x.DeleteAsync(It.IsAny<User>());
        _userManagerProxyMock = new Mock<IUserManagerProxy>();
        _usersService = new UsersService(_userManagerProxyMock.Object);
    }

    [Test]
    public async Task DeleteAsync__UserNotExists__ThrowsNotFoundException()
    {
        // Arrange
        _userManagerProxyMock.Setup(_getUserExpression).ReturnsAsync((User?)null);
        _userManagerProxyMock.Setup(_deleteUserExpression);
        var shouldThrow = () => _usersService.DeleteUserAsync(DefaultUserId);

        // Act & Assert
        await shouldThrow.Should().ThrowAsync<EntityNotFoundException>();

        _userManagerProxyMock.Verify(_getUserExpression, Times.Once);
        _userManagerProxyMock.Verify(_deleteUserExpression, Times.Never);
    }

    [Test]
    public async Task DeleteAsync_UserExists_UserDeleted()
    {
        // Arrange
        var user = new User()
        {
            Id = DefaultUserId,
            Email = "test@example.com",
            EmailConfirmed = true,
            PhoneNumber = "+12345678900",
            PhoneNumberConfirmed = true,
            UserName = "TestUser",
            LockoutEnabled = true,
            LockoutEnd = new DateTimeOffset(2099, 01, 01, 0, 0, 0, TimeSpan.Zero),
            TwoFactorEnabled = true
        };

        _userManagerProxyMock.Setup(_getUserExpression).ReturnsAsync(user);
        _userManagerProxyMock.Setup(_deleteUserExpression);

        // Act
        await _usersService.DeleteUserAsync(DefaultUserId);

        // Assert
        _userManagerProxyMock.Verify(_getUserExpression, Times.Once);
        _userManagerProxyMock.Verify(_deleteUserExpression, Times.Once);
    }
}
