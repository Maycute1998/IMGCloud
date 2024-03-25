namespace IMGCloud.Domain.Options;

public sealed class AmazonBulketOptions
{
    public string BucketName { get; set; } = string.Empty;
    public PrefixOptions Prefix { get; set; } = new();
    public string SecretAccessKey { get; set; } = string.Empty;
    public string AccessKey { get; set; } = string.Empty;
    public string Region { get; set; } = "ap-southeast-2";
}

public sealed class PrefixOptions
{
    public string Photos { get; set; } = "photos";
    public string Videos { get; set; } = "videos";
    public string Avatars { get; set; } = "avatars";
}