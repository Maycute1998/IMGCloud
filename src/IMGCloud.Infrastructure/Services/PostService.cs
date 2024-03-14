using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
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


    private Task<List<PostContext>> GetAllPostsAsync(CancellationToken cancellationToken = default)
    => _postRepository.GetAllPostsAsync(cancellationToken);

    Task<List<PostContext>>IPostService.GetAllPostsAsync(CancellationToken cancellationToken)
    =>this.GetAllPostsAsync(cancellationToken);
}
