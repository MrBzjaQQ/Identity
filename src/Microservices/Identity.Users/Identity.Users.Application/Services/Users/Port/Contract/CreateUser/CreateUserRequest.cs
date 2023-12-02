namespace Identity.Users.Application.Services.Users.Port.Contract.CreateUser;

/// <summary>
/// Request for user creation
/// </summary>
public sealed record CreateUserRequest
{
    public string UserName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string PasswordConfirm { get; init; } = string.Empty;

    public string? PhoneNumber { get; init; } = string.Empty;
}
