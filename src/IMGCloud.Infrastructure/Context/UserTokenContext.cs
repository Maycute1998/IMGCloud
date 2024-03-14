namespace IMGCloud.Infrastructure.Context;

public sealed class UserTokenContext
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? Token { get; set; }
    public DateTime? ExpireDate { get; set; }
    public bool IsActive { get; set; }
}
