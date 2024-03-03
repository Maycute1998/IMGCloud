using IMGCloud.Data.Entities;
using IMGCloud.Data.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Data.Context
{
    public class IMGCloudContext : DbContext
    {
        public IMGCloudContext() { }
        public IMGCloudContext(DbContextOptions<IMGCloudContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenConfiguration());
            modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostCollectionConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new PostImageConfiguration());
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostImage> PostImages { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
