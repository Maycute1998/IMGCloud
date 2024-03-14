using System.Text.Json.Serialization;

namespace IMGCloud.Domain.Cores;

public sealed class ErrorApiResult<T> : ApiResult<T>
{
    public ErrorApiResult()
    {
    }

    [JsonConstructor]
    public ErrorApiResult(T context, string? message = default)
        : base(false, context, message)
    {
        this.Context = context;
    }
}
