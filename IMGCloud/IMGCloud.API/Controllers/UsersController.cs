using IMGCloud.Application.Interfaces.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMGCloud.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync(string userName)
        {
            var data = await _userService.GetUserInfor(userName);

            return ExecuteResult(new
            {
                data.Status,
                data.Data,
                data.Message
            });
        }
    }
}
