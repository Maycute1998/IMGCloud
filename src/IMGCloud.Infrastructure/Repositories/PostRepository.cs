using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Requests;

namespace IMGCloud.Infrastructure.Repositories;

public sealed class PostRepository : RepositoryBase<Post, int>, IPostRepository
{
    public PostRepository(ImgCloudContext dbContext, IUnitOfWork unitOfWork)
        : base(dbContext, unitOfWork)
    {
    }

    private Task EditPostAsync(CreatePostRequest post, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private Task PressHeartAsync(CreatePostRequest post, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private Task CreatePostAsync(CreatePostRequest post, CancellationToken cancellationToken)
    {
        this.dbContext.Posts.Add(new()
        {
            Caption = post.Caption,
            CollectionId = post.CollectionId,
            Emotion = post.Emotion,
            Location = post.Location,
            PostPrivacy = post.PostPrivacy
        });

        return this.SaveChangesAsync(cancellationToken);
    }

    Task IPostRepository.CreatePostAsync(CreatePostRequest post, CancellationToken cancellationToken)
    => this.CreatePostAsync(post, cancellationToken);
    Task IPostRepository.EditPostAsync(CreatePostRequest post, CancellationToken cancellationToken)
    => this.EditPostAsync(post, cancellationToken);
    Task IPostRepository.PressHeartAsync(CreatePostRequest post, CancellationToken cancellationToken)
    => this.PressHeartAsync(post, cancellationToken);

}
