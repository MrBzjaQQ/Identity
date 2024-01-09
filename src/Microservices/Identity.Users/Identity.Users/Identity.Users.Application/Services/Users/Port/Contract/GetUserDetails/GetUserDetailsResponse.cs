
namespace Identity.Users.Application.Services.Users.Port.Contract.GetUserDetails;

public record GetUserDetailsResponse
{
    /// <summary>
    /// User identifier
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// User name
    /// </summary>
    public string? UserName { get; init; }

    /// <summary>
    /// User email
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// User's phone number
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Information about enabled second factor
    /// </summary>
    public bool IsTwoFactorEnabled { get; init; }

    /// <summary>
    /// Is lockout enabled
    /// </summary>
    public bool IsLockoutEnabled { get; init; }

    /// <summary>
    /// Is user's email confirmed
    /// </summary>
    public bool IsEmailConfirmed { get; init; }

    /// <summary>
    /// Is user's phone number confirmed
    /// </summary>
    public bool IsPhoneNumberConfirmed { get; init; }

    /// <summary>
    /// The date and time, in UTC, when any user lockout ends.
    /// </summary>
    public DateTimeOffset? LockoutEnd { get; init; }
}
