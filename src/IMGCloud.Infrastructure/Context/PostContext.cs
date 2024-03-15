using IMGCloud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Context
{
    public class PostContext
    {
        public int Id { get; set; }
        public string? Caption { get; set; }
        public string? Location { get; set; }
        public PostPrivacy PostPrivacy { get; set; }
        public string? ImagePath { get; set; }
        public string? UserName { get; set; }
        public string? UserAvatar { get; set; }
    }

    public class PostDetails : PostContext
    {
        public string? Emotion { get; set; }
        public int Heart { get; set; }
        public int Views { get; set; }
        public string? CollectionName { get; set; }
        public ICollection<CommentContext> Comments { get; set; } = new List<CommentContext>();
    }

    public class CommentContext
    {
        public string? Content { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
