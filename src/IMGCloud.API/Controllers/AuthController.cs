using IMGCloud.Domain.Cores;
using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Infrastructure.Services;
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
    public async Task<IActionResult> SigInAsync([FromBody] SignInContext model, CancellationToken cancellationToken = default)
    {
        var data = await _authService.SignInAsync(model, cancellationToken);

        return Ok(new AuthencationApiResult()
        {
            Token = data.Token,
            IsSucceeded = true
        });
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] CreateUserRequest model, CancellationToken cancellationToken = default)
    {
        var reponse = await _authService.SignUpAsync(model, cancellationToken);
        return Ok(new
        {
            IsSucceeded = true,
            Id = reponse.Context?.Id 
        });
    }

    [HttpPost]
    [Route("google-signin")]
    public async Task<IActionResult> GoogleSiginAsync(GoogleAuthenticationContext context)
    {
        var data = await _googleService.VerifyTokenAsync(context);

        return Ok(data);
    }
}
