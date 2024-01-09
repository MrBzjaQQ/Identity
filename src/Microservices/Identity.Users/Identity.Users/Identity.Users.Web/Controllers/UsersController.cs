using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
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

    [HttpGet("getById/{id}")]
    public async Task<GetUserDetailsResponse> GetUser(string id)
    {
        return await _usersService.GetByIdAsync(id);
    }
}
