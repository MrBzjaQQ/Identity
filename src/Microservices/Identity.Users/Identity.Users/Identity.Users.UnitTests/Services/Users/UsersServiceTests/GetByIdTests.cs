using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users;
using Identity.Users.Domain.Entities;
using Moq;
using System.Linq.Expressions;
using FluentAssertions;
using Identity.Users.Resources;
using Identity.Users.Application.Services.Users.Port.Contract.GetUserDetails;
using Identity.Users.Application.Exceptions;

namespace Identity.Users.UnitTests.Services.Users.UsersServiceTests;

public class GetByIdTests
{
    private string DefaultUserId = Guid.NewGuid().ToString();
    private Expression<Func<IUserManagerProxy, Task<User?>>> _getUserExpression;
    private Mock<IUserManagerProxy> _userManagerProxyMock;
    private UsersService _usersService;

    [SetUp]
    public void SetUp()
    {
        _getUserExpression = proxy => proxy.FindByIdAsync(DefaultUserId);
        _userManagerProxyMock = new Mock<IUserManagerProxy>();
        _usersService = new UsersService(_userManagerProxyMock.Object);
    }

    [Test]
    public async Task GetByIdAsync_UserExists_ReturnsUser()
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

        var userDetails = new GetUserDetailsResponse()
        {
            Id = DefaultUserId,
            Email = user.Email,
            IsEmailConfirmed = user.EmailConfirmed,
            IsLockoutEnabled = user.LockoutEnabled,
            LockoutEnd = user.LockoutEnd,
            IsPhoneNumberConfirmed = user.PhoneNumberConfirmed,
            IsTwoFactorEnabled = user.TwoFactorEnabled,
            PhoneNumber = user.PhoneNumber,
            UserName = user.UserName
        };

        _userManagerProxyMock.Setup(_getUserExpression)
            .ReturnsAsync(user);

        // Act
        var result = await _usersService.GetByIdAsync(DefaultUserId);

        // Assert
        result.Should().BeEquivalentTo(userDetails);
    }

    [Test]
    public async Task GetByIdAsync_UserNotFound_Throws404()
    {
        // Arrange
        _userManagerProxyMock.Setup(_getUserExpression)
            .ReturnsAsync((User?)null);

        // Act
        var shouldThrow = () => _usersService.GetByIdAsync(DefaultUserId);

        // Assert
        var exceptionAssert = await shouldThrow.Should().ThrowAsync<EntityNotFoundException>();
        exceptionAssert.WithMessage(string.Format(ErrorMessages.UserNotFoundTemplate, DefaultUserId));
    }
}
