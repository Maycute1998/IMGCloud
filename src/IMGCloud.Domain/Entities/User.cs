namespace IMGCloud.Domain.Entities;

public class User : EntityBase<int>
{
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public UserDetail? UserInfos { get; set; }
    public UserToken? UserTokens { get; set; }

    public virtual ICollection<Post>? Posts { get; set; } = new List<Post>();
}
