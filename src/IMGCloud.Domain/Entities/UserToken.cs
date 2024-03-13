namespace IMGCloud.Domain.Entities;

public class UserToken : EntityBase<int>
{
    public int? UserId { get; set; }
    public string? Token { get; set; }
    public DateTime? ExpiredDate { get; set; }
    public User? User { get; set; }
}
