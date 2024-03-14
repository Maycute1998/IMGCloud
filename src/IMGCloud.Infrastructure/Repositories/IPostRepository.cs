using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Repositories;

public interface IPostRepository
{
    Task<List<Post>> GetAllPostsAsync(CancellationToken cancellationToken = default);
    Task CreatePostAsync(CreatePostRequest post, CancellationToken cancellationToken = default);
    Task EditPostAsync(CreatePostRequest post, CancellationToken cancellationToken = default);
    Task PressHeartAsync(CreatePostRequest post, CancellationToken cancellationToken = default);
}
