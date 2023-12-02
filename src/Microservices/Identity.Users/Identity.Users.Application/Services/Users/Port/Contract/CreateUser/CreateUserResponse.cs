namespace Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
public sealed record CreateUserResponse
{
    public string? UserId { get; init; }

    public IList<IdentityErrorResponse> Errors { get; init; } = Array.Empty<IdentityErrorResponse>();

    public bool IsSucceeded { get; init; }
}
