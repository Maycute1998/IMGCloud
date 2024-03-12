namespace IMGCloud.Domain.Cores;

public class AuthencationApiResult<T> : ApiResult<T>
{
    public AuthencationApiResult(bool isSucceeded, T context, string? message = null)
        : base(isSucceeded, context, message)
    {
    }
}
