namespace IMGCloud.Infrastructure.Context;

public sealed class S3ObjectContext
{
    public string? Name { get; set; }
    public string? PresignedUrl { get; set; }
}
