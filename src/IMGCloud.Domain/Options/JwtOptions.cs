namespace IMGCloud.Domain.Options;

public sealed class JwtOptions
{
    public string? ValidIssuer { get; set; }
    public string? ValidAudience { get; set; }
    public string? SecretKey { get; set; }
}
