namespace IMGCloud.Domain.Entities;

public class User : EntityBase<int>
{
    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public UserInfo? UserInfos { get; set; }
    public UserToken? UserTokens { get; set; }

    public virtual ICollection<Post>? Posts { get; set; } = new List<Post>();
}
