﻿using IMGCloud.Infrastructure.Services;
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


        [HttpPost("upload-avatar")]
        public async Task<IActionResult> UploadFileAsync([FromForm] string base64String, CancellationToken cancellationToken = default)
        {
            await service.UploadFileAsync(base64String, true, cancellationToken);
            return this.Ok();
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
