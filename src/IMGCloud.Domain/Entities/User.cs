namespace IMGCloud.Domain.Entities;

public class User : EntityBase<int>
{
    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string? ResetPasswordToken { get; set; } = string.Empty;
    public DateTime? ResetPasswordTokenExpiration { get; set; }

    public UserDetail? UserDetails { get; set; }
    public UserToken? UserTokens { get; set; }

    public virtual ICollection<Post>? Posts { get; set; } = new List<Post>();
}
