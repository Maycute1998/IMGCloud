using IMGCloud.Application.Implement.Auth;
using IMGCloud.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using IMGCloud.API.ViewModels;
using IMGCloud.Application.Interfaces.Auth;

namespace IMGCloud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SiginAsync([FromForm] SigInVM model)
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
        public async Task<IActionResult> RegisterAsync([FromForm] CreateUserVM model)
        {
            var data = await _authService.SignUpAsync(model);
            return ExecuteResult(data.Status, data.Message);
        }

        
    }
}
