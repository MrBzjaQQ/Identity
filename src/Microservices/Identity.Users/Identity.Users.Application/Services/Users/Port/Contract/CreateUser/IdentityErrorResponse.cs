namespace Identity.Users.Application.Services.Users.Port.Contract.CreateUser;

public sealed record IdentityErrorResponse
{
    public string Code { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}
