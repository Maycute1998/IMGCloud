using IMGCloud.Application.Interfaces;
using IMGCloud.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMGCloud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseController
    {
        private readonly IPostService _postService;


        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public string Get()
        {
            return "Test";
        }
    }
}
