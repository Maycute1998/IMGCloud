namespace IMGCloud.Domain.Options;

public sealed class TokenOptions
{
    public string SecurityKey { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int Expiry { get; set; }
    public string ClaimKey { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}