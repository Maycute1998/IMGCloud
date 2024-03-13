using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Options;

public sealed class AmazonBulketOptions
{
    public string Profile { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public Credentials? Credentials { get; set; }
    public AwsConfig? AwsConfig { get; set; }
}

public sealed class Credentials
{
    public string AccessKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}

public sealed class AwsConfig
{
    public string BucketName { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
}