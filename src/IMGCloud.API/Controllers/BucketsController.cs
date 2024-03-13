using Amazon.S3;
using Amazon.S3.Model;
using IMGCloud.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace IMGCloud.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketsController : ControllerBase
    {
        private readonly IAmazonBucketService service;

        public BucketsController(IAmazonBucketService service)
        {
            this.service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            var result = await service.UploadFileAsync(file);
            return string.IsNullOrWhiteSpace(result) ? this.Ok(result) : this.BadRequest();

        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllFilesAsync()
        {
            var result = await this.service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("get-by-key")]
        public async Task<IActionResult> GetFileByKeyAsync(string bucketName, string key)
        {
            var result = await this.service.GetAsync(key);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFileAsync(string key)
        {
            await this.service.DeleteAsync(key);
            return Ok();
        }
    }
}
