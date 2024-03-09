namespace IMGCloud.Domain.Entities;

public class PostImage : EntityBase<int>
{
    public int PostId { get; set; }
    public string? ImagePath { get; set; }
    public string? Caption { get; set; }
    public long FileSize { get; set; }
    public Post? Post { get; set; }
}
