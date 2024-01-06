using Identity.Users.Application.Services.Users.Port;
using Identity.Users.Application.Services.Users.Port.Contract.CreateUser;
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
}
