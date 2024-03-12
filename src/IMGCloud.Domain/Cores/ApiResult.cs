using System.Text.Json.Serialization;

namespace IMGCloud.Domain.Cores;

public class ApiResult<T> : ApiResult
{
    public T? Context { get; set; }

    public ApiResult()
    {
    }

    [JsonConstructor]
    public ApiResult(bool isSucceeded, T context, string? message = default)
        : base(isSucceeded, message)
    {
        this.Context = context;
    }

    public T GetResult()
    {
        return this.Context!;
    }
}

public class ApiResult
{
    public string? Message { get; set; }
    public bool IsSucceeded { get; set; }

    public ApiResult()
    {
    }

    [JsonConstructor]
    public ApiResult(bool isSucceeded, string? message = default)
    {
        this.Message = message;
        this.IsSucceeded = isSucceeded;
    }
}
