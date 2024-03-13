using Microsoft.AspNetCore.Mvc;
using IMGCloud.Infrastructure.Requests;
using IMGCloud.Infrastructure.Services;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Domain.Cores;

namespace IMGCloud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SigInAsync([FromBody] SignInContext model)
        {
            var data = await _authService.SignInAsync(model);

            return Ok(new ApiResult<AuthencationApiResult>()
            {
                Context = data,
                IsSucceeded = true
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] CreateUserRequest model)
        {
            await _authService.SignUpAsync(model);
            return Ok();
        }


    }
}
