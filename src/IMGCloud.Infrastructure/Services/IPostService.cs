using IMGCloud.Infrastructure.Context;

namespace IMGCloud.Infrastructure.Services;

public interface IPostService
{
    Task<List<PostContext>> GetAllPostsAsync(CancellationToken cancellationToken = default);
    Task<PostDetails> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
