using IMGCloud.Domain.Entities;
using IMGCloud.Infrastructure.Context;
using IMGCloud.Infrastructure.Extensions;
using IMGCloud.Infrastructure.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;

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

    private async Task<List<PostContext>> GetAllPostsAsync(CancellationToken cancellationToken = default)
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
                ImagePath = post.PostImages!.First().ImagePath
            }.MapFor(post))
            .ToListAsync(cancellationToken);
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

    Task<List<PostContext>> IPostRepository.GetAllPostsAsync(CancellationToken cancellationToken = default)
    => this.GetAllPostsAsync(cancellationToken);
}
