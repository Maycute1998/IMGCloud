namespace IMGCloud.Domain.Entities;

public class Collection : EntityBase<int>
{
    public int UserId { get; set; }
    public string? CollectionName { get; set; }
    public string? Photo { get; set; }
    public string? Description { get; set; }
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
