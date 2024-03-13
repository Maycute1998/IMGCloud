using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Options;

public sealed class AmazonBulketOptions
{
    public string BucketName { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
}