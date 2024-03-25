using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Extensions;
using IMGCloud.Infrastructure.Requests;
using Microsoft.EntityFrameworkCore;

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

    private async Task<List<PostContext>> GetAllPostsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Posts
            .Include(x => x.PostImages)
            .Include(x => x.Users)
                .ThenInclude(u => u.UserDetails)
            .Where(x => x.Status == Status.Active)
            .Select(post => new PostContext
            {
                UserName = post.Users!.UserName,
                UserAvatar = post.Users.UserDetails!.Photo,
                ImagePath = post.PostImages!.FirstOrDefault().ImagePath
            }.MapFor(post))
            .ToListAsync(cancellationToken);
    }

    private Task CreatePostAsync(CreatePostRequest post, CancellationToken cancellationToken)
    {
        var postImage = new PostImage
        {
            ImagePath = post.ImagePath
        };
        var postEnity = new Post();
        this.dbContext.PostImages.Add(postImage);
        this.dbContext.Posts.Add(postEnity).MapFor(post);

        return this.SaveChangesAsync(cancellationToken);
    }

    private async Task<PostDetails?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var post = await dbContext.Posts
            .Include(x => x.PostImages)
            .Include(x => x.Users)
                .ThenInclude(u => u.UserDetails)
            .Include(x => x.Comments)
            .Include(x => x.Collection)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Status == Status.Active && x.Id == id, cancellationToken);
        return new PostDetails
        {
            UserName = post.Users!.UserName,
            UserAvatar = post.Users.UserDetails!.Photo,
            ImagePath = post.PostImages!.FirstOrDefault().ImagePath,
            CollectionName = post.Collection!.CollectionName,
            Comments = post.Comments!.Select(x => new CommentContext { Content = x.Content, UserId = x.UserId }).ToList(),
        }.MapFor(post);
    }

    Task IPostRepository.CreatePostAsync(CreatePostRequest post, CancellationToken cancellationToken)
    => this.CreatePostAsync(post, cancellationToken);
    Task IPostRepository.EditPostAsync(CreatePostRequest post, CancellationToken cancellationToken)
    => this.EditPostAsync(post, cancellationToken);
    Task IPostRepository.PressHeartAsync(CreatePostRequest post, CancellationToken cancellationToken)
    => this.PressHeartAsync(post, cancellationToken);

    Task<List<PostContext>> IPostRepository.GetAllPostsAsync(CancellationToken cancellationToken)
    => this.GetAllPostsAsync(cancellationToken);

    Task<PostDetails?> IPostRepository.GetByIdAsync(int id, CancellationToken cancellationToken)
    => this.GetByIdAsync(id, cancellationToken);
}
