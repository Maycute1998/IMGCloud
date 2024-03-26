using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Repositories;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IAmazonBucketService _amazonBucketService;

    public PostService(IPostRepository postRepository, IAmazonBucketService amazonBucketService)
    {
        _postRepository = postRepository;
        _amazonBucketService = amazonBucketService;
    }

    private Task<PostDetails?> GetByIdAsync(int id, CancellationToken cancellationToken)
    => _postRepository.GetByIdAsync(id, cancellationToken);

    private Task<List<PostContext>> GetAllPostsAsync(CancellationToken cancellationToken)
    => _postRepository.GetAllPostsAsync(cancellationToken);

    private async Task CreateAsync(CreatePostRequest post, bool isPost, CancellationToken cancellationToken = default)
    {
        var fileUrl = await _amazonBucketService.UploadFileAsync(post.ImagePath, isPost, cancellationToken);
        await _postRepository.CreatePostAsync(post, cancellationToken);
    }

    private async Task<List<PostDetails>> GetByCollectionIdAsync(int id, CancellationToken cancellationToken)
    => await _postRepository.GetByCollectionIdAsync(id, cancellationToken);

    Task<List<PostContext>>IPostService.GetAllPostsAsync(CancellationToken cancellationToken)
    =>this.GetAllPostsAsync(cancellationToken);

    Task<PostDetails?> IPostService.GetByIdAsync(int id, CancellationToken cancellationToken)
    => this.GetByIdAsync(id, cancellationToken);

    Task<List<PostDetails>> IPostService.GetByCollectionIdAsync(int id, CancellationToken cancellationToken)
    => this.GetByCollectionIdAsync(id, cancellationToken);

    Task IPostService.CreateAsync(CreatePostRequest post, bool isPost, CancellationToken cancellationToken)
    => this.CreateAsync(post, isPost, cancellationToken);
}
