namespace IMGCloud.Domain.Cores;

public sealed class AuthencationApiResult<T> : ApiResult<T>
{
    public AuthencationApiResult()
    {
    }

    public AuthencationApiResult(bool isSucceeded, T context, string? message = null)
        : base(isSucceeded, context, message)
    {
    }
}

public sealed class AuthencationApiResult
{
    public string Token { get; set; } = string.Empty;
}