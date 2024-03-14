using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Services;

public interface IPostService
{
    Task<List<PostContext>> GetAllPostsAsync(CancellationToken cancellationToken = default);
}
