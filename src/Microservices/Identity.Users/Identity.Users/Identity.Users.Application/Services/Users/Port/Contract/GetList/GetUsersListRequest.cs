namespace Identity.Users.Application.Services.Users.Port.Contract.GetList;
public sealed record GetUsersListRequest
{
    public int Take { get; init; }
    public int Skip { get; init; }
}
