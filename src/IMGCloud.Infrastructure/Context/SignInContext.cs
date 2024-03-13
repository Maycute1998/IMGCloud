namespace IMGCloud.Infrastructure.Context;

public sealed class SignInContext
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
