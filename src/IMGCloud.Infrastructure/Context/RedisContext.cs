namespace IMGCloud.Infrastructure.Context;

public sealed class RedisContext
{
    public string? RedisKey { get; set; }
    public string? Token { get; set; }
    public DateTime? ExpiredDate { get; set; }
}
