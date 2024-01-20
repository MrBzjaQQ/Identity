namespace Identity.Users.Application.Services.Users.Port.Contract.GetList;
public sealed record UserListItem
{
    public string Id { get; init; } = string.Empty;
    public string? UserName { get; init; } = string.Empty;
    public string? Email { get; init; } = string.Empty;
}
