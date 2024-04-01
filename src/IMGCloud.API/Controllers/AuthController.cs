using IMGCloud.Domain.Cores;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMGCloud.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly IGoogleService _googleService;

    public AuthController(IAuthenticationService authService, IGoogleService googleService)
    {
        _authService = authService;
        _googleService = googleService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> SignInAsync([FromBody] SignInContext model, CancellationToken cancellationToken = default)
    {
        var data = await _authService.SignInAsync(model, cancellationToken);
        return Ok(data);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserRequest model, CancellationToken cancellationToken = default)
    {
        try
        {
            await _authService.SignUpAsync(model, cancellationToken);
            return Ok(new ApiResult<string>()
            {
                IsSucceeded = true,
                Context = model.UserName,
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorApiResult<string>()
            {
                Message = ex.Message,
            });
        }
    }

    [HttpPost]
    [Route("signout")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> SignOutAsync(CancellationToken cancellationToken = default)
    {
        await _authService.SignOutAsync(cancellationToken);
        return Ok();
    }

    [HttpPost]
    [Route("google-signin")]
    public async Task<IActionResult> GoogleSiginAsync(GoogleAuthenticationContext context)
    {
        var data = await _googleService.VerifyTokenAsync(context);

        return Ok(data);
    }
}
