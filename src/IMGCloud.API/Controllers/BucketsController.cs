using Amazon.S3;
using Amazon.S3.Model;
using IMGCloud.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

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
        public async Task<IActionResult> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            var result = await service.UploadFileAsync(file, cancellationToken);
            return string.IsNullOrWhiteSpace(result) ? this.Ok(result) : this.BadRequest();

        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllFilesAsync(CancellationToken cancellationToken = default)
        {
            var result = await this.service.GetAllAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("get-by-key")]
        public async Task<IActionResult> GetFileByKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            var result = await this.service.GetAsync(key, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFileAsync(string key, CancellationToken cancellationToken = default)
        {
            await this.service.DeleteAsync(key, cancellationToken);
            return Ok();
        }
    }
}
