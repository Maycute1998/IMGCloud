using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using IMGCloud.Domain.Options;
using IMGCloud.Infrastructure.Context;
using Microsoft.AspNetCore.Http;

namespace IMGCloud.Infrastructure.Services;

public sealed class AmazonBucketService : IAmazonBucketService
{
    private readonly IAmazonS3 _s3Client;
    private readonly AmazonBulketOptions bulketOptions;
    public AmazonBucketService(IAmazonS3 s3Client, AmazonBulketOptions amazonBulketOptions)
    {
        this._s3Client = s3Client;
        this.bulketOptions = amazonBulketOptions;
    }

    private async Task DeleteAsync(string key, CancellationToken cancellationToken)
    {
        var isExistBucket = await this.IsExistedAsync();
        if (!isExistBucket)
        {
            return;
        }

        await _s3Client.DeleteObjectAsync(this.bulketOptions.BucketName, key, cancellationToken);
    }

    private async Task<IEnumerable<S3ObjectContext>> GetAllAsync(CancellationToken cancellationToken)
    {
        var bucketExists = await this.IsExistedAsync();
        if (bucketExists)
        {
            var request = new ListObjectsV2Request()
            {
                BucketName = this.bulketOptions.BucketName,
                Prefix = this.bulketOptions.Prefix.Photos
            };
            var result = await _s3Client.ListObjectsV2Async(request, cancellationToken);
            return result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = this.bulketOptions.BucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };
                return new S3ObjectContext()
                {
                    Name = s.Key.ToString(),
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                };
            });
        }

        return Enumerable.Empty<S3ObjectContext>();

    }
    private async Task<GetObjectResponse?> GetAsync(string key, CancellationToken cancellationToken)
    {
        var isExistBucket = await this.IsExistedAsync();
        if (isExistBucket)
        {
            return await _s3Client.GetObjectAsync(this.bulketOptions.BucketName, key, cancellationToken);
        }

        return default;
    }

    private Task<bool> IsExistedAsync()
    {
        return AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, this.bulketOptions.BucketName);
    }

    private async Task<string> UploadFileAsync(string base64String, bool isPost = true, CancellationToken cancellationToken)
    {
        var photoUrl = string.Empty;
        var isExistBucket = await IsExistedAsync();
        if (isExistBucket)
        {
            var prefix = isPost? this.bulketOptions.Prefix.Photos : this.bulketOptions.Prefix.Avatars;
            base64String = base64String.Replace("data:image/png;base64,", "");
            byte[] bytes = Convert.FromBase64String(base64String);

            var request = new PutObjectRequest
            {
                BucketName = this.bulketOptions.BucketName,
                Key = string.IsNullOrEmpty(prefix) ? $"{Guid.NewGuid()}.jpg" : $"{prefix?.TrimEnd('/')}/{Guid.NewGuid()}.jpg",
            };
            using var ms = new MemoryStream(bytes);
            request.InputStream = ms;
            await _s3Client.PutObjectAsync(request, cancellationToken);
            photoUrl = $"https://{request.BucketName}.s3.{this.bulketOptions.Region}.amazonaws.com/{request.Key}";
        }
        return photoUrl;
    }

    Task<IEnumerable<S3ObjectContext>> IAmazonBucketService.GetAllAsync(CancellationToken cancellationToken)
    => this.GetAllAsync(cancellationToken);
    Task<bool> IAmazonBucketService.IsExistedAsync()
    => this.IsExistedAsync();
    Task<GetObjectResponse?> IAmazonBucketService.GetAsync(string key, CancellationToken cancellationToken)
    => GetAsync(key, cancellationToken);
    Task IAmazonBucketService.DeleteAsync(string key, CancellationToken cancellationToken)
    => DeleteAsync(key, cancellationToken);
    Task<string> IAmazonBucketService.UploadFileAsync(string base64String, bool isPost, CancellationToken cancellationToken)
    => UploadFileAsync(base64String, isPost, cancellationToken);
}


