using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMGCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var data = await _userService.GetUserDetailByUserNameAsync(name, cancellationToken);
        return Ok(new ApiResult<UserDetailContext>()
        {
            Context = data,
            IsSucceeded = true,
        });
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateUserInfoAsync([FromBody] UserDetailsRequest userInfo, CancellationToken cancellationToken = default)
    {
        await _userService.CreateUserDetailAsync(userInfo, cancellationToken);
        return Ok();
    }
}
