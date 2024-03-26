using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Services;

public interface IPostService
{
    Task<List<PostContext>> GetAllPostsAsync(CancellationToken cancellationToken = default);
    Task<PostDetails> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(CreatePostRequest post, bool isPost = true, CancellationToken cancellationToken = default);
    Task<List<PostDetails>> GetByCollectionIdAsync(int id, CancellationToken cancellationToken = default);
}
