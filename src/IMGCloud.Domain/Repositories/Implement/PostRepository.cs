using IMGCloud.Data.Context;
using IMGCloud.Data.Entities;
using IMGCloud.Domain.Models;
using IMGCloud.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace IMGCloud.Domain.Repositories.Implement
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly IMGCloudContext _context;
        private readonly ILogger<PostRepository> _logger;
        private readonly IStringLocalizer<PostRepository> _stringLocalizer;
        private readonly string className = typeof(PostRepository).FullName ?? string.Empty;

        public PostRepository(ILogger<PostRepository> logger,
            IMGCloudContext context,
            IStringLocalizer<PostRepository> stringLocalizer) : base(context)
        {
            _logger = logger;
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        public Task<ResponeVM> CreatePost(CreatePostRequest post)
        {
            throw new NotImplementedException();
        }

        public Task<ResponeVM> EditPost(CreatePostRequest post)
        {
            throw new NotImplementedException();
        }

        public Task<ResponeVM> PressingHeart(CreatePostRequest post)
        {
            throw new NotImplementedException();
        }
    }
}
