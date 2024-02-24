using Identity.Users.Application.Services.Shared.Port.Contact;
using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
using Identity.Users.Application.Services.Users.Port.Contract.GetList;
using Identity.Users.Application.Services.Users.Port.Contract.GetUserDetails;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Users.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpPost("createUser")]
    public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
    {
        return await _usersService.CreateUserAsync(request);
    }

    [HttpGet("{id}")]
    public async Task<GetUserDetailsResponse> GetUser(string id)
    {
        return await _usersService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<PagedListResponse<UserListItem>> GetList(GetUsersListRequest request)
    {
        return await _usersService.GetListAsync(request, HttpContext.RequestAborted);
    }

    [HttpDelete("{id}")]
    public async Task DeleteUser(string id)
    {
        await _usersService.DeleteUserAsync(id, HttpContext.RequestAborted);
    }
}
