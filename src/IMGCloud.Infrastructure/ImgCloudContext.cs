using IMGCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMGCloud.Infrastructure;

public sealed class ImgCloudContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostImage> PostImages { get; set; }
    public DbSet<UserToken> UserTokens { get; set; }
    public DbSet<UserDetail> UserDetails { get; set; }
    public DbSet<User> Users { get; set; }

    public ImgCloudContext(DbContextOptions<ImgCloudContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ImgCloudContext).Assembly);
    }
}
