namespace IMGCloud.Infrastructure.Context;

public sealed class GoogleAuthenticationContext
{
    public string Provider { get; set; } = string.Empty;
    public string GoogleTokenId { get; set; } = string.Empty;
}
