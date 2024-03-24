using IMGCloud.Domain.Cores;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace IMGCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ISendMailService _sendMailService;
    public UsersController(IUserService userService, ISendMailService sendMailService)
    {
        _userService = userService;
        _sendMailService = sendMailService;
    }

    [HttpGet]
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

    [HttpPost]
    [Route("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email, CancellationToken cancellationToken = default)
    {
        var resetToken = await _userService.ForgotPasswordAsync(email, cancellationToken);
        await _sendMailService.SendResetPasswordEmail(email, resetToken);
        return Ok();
    }

    [HttpPost]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
    {
        await _userService.ResetPasswordAsync(model);
        return Ok();
    }
}
