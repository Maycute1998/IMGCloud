namespace IMGCloud.Domain.Options;

public sealed class AmazonBulketOptions
{
    public string BucketName { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
    public string SecretAccessKey { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
}