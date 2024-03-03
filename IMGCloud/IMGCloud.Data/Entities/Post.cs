using IMGCloud.Data.Enums;

namespace IMGCloud.Data.Entities
{
    public class Post : BaseEntity
    {
        public int? CollectionId { get; set; }
        public string? Caption { get; set; }
        public string? Location  { get; set; }
        public string? Emotion { get; set; }
        public PostPrivacy PostPrivacy { get; set; }
        public int? Heart { get; set; }
        public User? Users { get; set; }
        public Collection? Collection { get; set; } 
        public ICollection<PostImage>? PostImages { get; set; } = new List<PostImage>();
        public ICollection<Comment>? Comments { get; set; } = new List<Comment>(); // List<Comment> PostTags { get; set; }
    }
}
