using IMGCloud.Application.Implement.Auth;
using IMGCloud.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Xml.Linq;

namespace IMGCloud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticationService _authService;

        public AuthController(AuthenticationService authService)
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


        public virtual IActionResult ExecuteResult<TOption>(TOption option)
        {
            return new ObjectResult(option);
        }

        public virtual IActionResult ExecuteResult(bool Status, string Message)
        {
            var Result = new BaseOptions();
            try
            {
                if (Status)
                {
                    Result.Data = null;
                    Result.Status = Status;
                    Result.Message = Message;
                    Result.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    Result.Data = null;
                    Result.Status = Status;
                    Result.Message = Message;
                    Result.StatusCode = HttpStatusCode.InternalServerError;
                }
                return new ObjectResult(Result);
            }
            catch (Exception ex)
            {
                Result.Data = null;
                Result.Status = false;
                Result.Message = string.Concat("Bad request", ex.Message);
                Result.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(Result);
            }
        }
    }
}
