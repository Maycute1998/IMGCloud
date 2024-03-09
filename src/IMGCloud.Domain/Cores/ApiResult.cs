using System.Text.Json.Serialization;

namespace IMGCloud.Domain.Cores;

public class ApiResult<T>
{
    public string? Message { get; set; }
    public bool IsSucceeded { get; set; }
    public T? Result { get; set; }
    public ApiResult()
    {
    }

    [JsonConstructor]
    public ApiResult(bool isSucceeded, string? message = default)
    {
        this.Message = message;
        this.IsSucceeded = isSucceeded;
    }

    public ApiResult(bool isSucceeded, T data, string? message = default)
    {
        this.Result = data;
        this.IsSucceeded = isSucceeded;
        this.Message = message;
    }

    public T GetResult()
    {
        return this.Result!;
    }
}
