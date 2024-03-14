using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Services;

public interface IPostService
{
    Task<List<Post>> GetAllPostsAsync(CancellationToken cancellationToken = default);
}
