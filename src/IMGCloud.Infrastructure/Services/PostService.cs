using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Repositories;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }


    private Task<List<Post>> GetAllPostsAsync(CancellationToken cancellationToken = default)
    => _postRepository.GetAllPostsAsync(cancellationToken);

    Task<List<Post>>IPostService.GetAllPostsAsync(CancellationToken cancellationToken)
    =>this.GetAllPostsAsync(cancellationToken);
}
