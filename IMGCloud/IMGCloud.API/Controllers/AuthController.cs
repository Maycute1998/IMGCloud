using IMGCloud.Application.Implement.Auth;
using IMGCloud.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using IMGCloud.API.ViewModels;
using IMGCloud.Application.Interfaces.Auth;

namespace IMGCloud.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authService;
        private readonly IAuthExternalService _authExternalService;

        public AuthController(IAuthenticationService authService, IAuthExternalService authExternalService)
        {
            _authService = authService;
            _authExternalService = authExternalService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SiginAsync([FromBody] SigInVM model)
        {
            var data = await _authService.SignInAsync(model);
            
            return ExecuteResult(new
            {
                data.Status,
                data.Message,
                data.Token
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserVM model)
        {
            var data = await _authService.SignUpAsync(model);
            return ExecuteResult(data.Status, data.Message);
        }

        [HttpPost]
        [Route("google-signin")]
        public async Task<IActionResult> GoogleSiginAsync(Application.ViewModels.Auth.ExternalAuthVM externalAuthVM)
        {
            var data = await _authExternalService.VerifyGoogleToken(externalAuthVM);

            return Ok(data);
        }

    }
}
