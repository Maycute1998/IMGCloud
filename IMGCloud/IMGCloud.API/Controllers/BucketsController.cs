using Amazon.S3;
using Amazon.S3.Model;
using IMGCloud.API.ViewModels;
using IMGCloud.Application.ViewModels.Auth;
using Microsoft.AspNetCore.Mvc;

namespace IMGCloud.API.Controllers
{
    [Route("api/buckets")]
    [ApiController]
    public class BucketsController : ControllerBase
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _configuration;
        private string bucketName;
        private string prefix;

        public BucketsController(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _configuration = configuration;
            bucketName = _configuration["AWS:AWSConfig:bucketName"];
            prefix = _configuration["AWS:AWSConfig:prefix"];
        }

        //Upload Files to AWS S3
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            var isExistBucket = await DoesS3BucketExistAsync();
            if (!isExistBucket.Status)
            {
                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
                    InputStream = file.OpenReadStream()
                };
                request.Metadata.Add("Content-Type", file.ContentType);
                await _s3Client.PutObjectAsync(request);
                return Ok($"File {prefix}/{file.FileName} uploaded to S3 successfully!");
            }
            return NotFound($"Bucket {bucketName} does not exist.");
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAvatarAsync([FromForm]string base64String)
        {
            try
            {
                base64String = base64String.Replace("data:image/png;base64,", "");
                byte[] bytes = Convert.FromBase64String(base64String);

                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = string.IsNullOrEmpty(prefix) ? "avatar.jpg" : $"{ prefix?.TrimEnd('/')}/avatar.jpg",
                };
                using (var ms = new MemoryStream(bytes))
                {
                    request.InputStream = ms;
                    await _s3Client.PutObjectAsync(request);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("AWS Fail");
            }
            return Ok();
        }


        //Get All the Files in an AWS S3 
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllFilesAsync()
        {
            var bucketExists = await DoesS3BucketExistAsync();
            if (bucketExists.Status)
            {
                var request = new ListObjectsV2Request()
                {
                    BucketName = bucketName,
                    Prefix = prefix
                };
                var result = await _s3Client.ListObjectsV2Async(request);
                var s3Objects = result.S3Objects.Select(s =>
                {
                    var urlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = bucketName,
                        Key = s.Key,
                        Expires = DateTime.UtcNow.AddMinutes(1)
                    };
                    return new S3ObjectVM()
                    {
                        Name = s.Key.ToString(),
                        PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                    };
                });
                return Ok(s3Objects);
            }
            return NotFound($"Bucket {bucketName} does not exist.");
        }

        //Download Files from AWS S3
        [HttpGet("get-by-key")]
        public async Task<IActionResult> GetFileByKeyAsync(string bucketName, string key)
        {
            var isExistBucket = await DoesS3BucketExistAsync();
            if (isExistBucket.Status)
            {
                var s3Object = await _s3Client.GetObjectAsync(bucketName, key);
                File(s3Object.ResponseStream, s3Object.Headers.ContentType);
            }
            return NotFound($"Bucket {bucketName} does not exist.");
        }

        //Delete Files from AWS S3
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFileAsync(string key)
        {
            var isExistBucket = await DoesS3BucketExistAsync();
            if (isExistBucket.Status)
            {
                await _s3Client.DeleteObjectAsync(bucketName, key);

            }
            return NotFound($"Bucket {bucketName} does not exist.");
        }

        #region[private methods]
        private async Task<ResponeVM> DoesS3BucketExistAsync()
        {
            var res = new ResponeVM();
            try
            {
                var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
                if (!bucketExists)
                {
                    res.Message = $"Bucket {bucketName} does not exist";
                    res.Status = false;
                }
                else
                {
                    res.Message = $"Bucket {bucketName} exists";
                    res.Status = true;
                }
            }
            catch (Exception ex)
            {
                res.Message = $"An error occurred: {ex.Message}";
                res.Status = false;
            }
            return res;
        }

        #endregion
    }
}
