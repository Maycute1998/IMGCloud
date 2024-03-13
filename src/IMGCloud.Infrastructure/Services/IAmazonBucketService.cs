using Amazon.S3.Model;
using IMGCloud.Infrastructure.Context;
using Microsoft.AspNetCore.Http;

namespace IMGCloud.Infrastructure.Services;

public interface IAmazonBucketService
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);
    Task<IEnumerable<S3ObjectContext>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> IsExistedAsync();
    Task<GetObjectResponse?> GetAsync(string key, CancellationToken cancellationToken = default);
    Task DeleteAsync(string key, CancellationToken cancellationToken = default);
}