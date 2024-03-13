namespace IMGCloud.Domain.Entities
{
    public class Comment : EntityBase<int>
    {
        public string? Content { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }

    }
}