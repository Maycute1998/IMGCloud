namespace IMGCloud.Domain.Cores;

public sealed class AuthencationApiResult : ApiResult
{
    public string Token { get; set; } = string.Empty;
    public AuthencationApiResult()
    {
    }

    public AuthencationApiResult(bool isSucceeded, string? message = default)
        : base(isSucceeded, message)
    {
    }
}

public sealed class AuthencationResult
{
    public string Token { get; set; } = string.Empty;
}