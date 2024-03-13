using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Context;

public sealed class S3ObjectContext
{
    public string? Name { get; set; }
    public string? PresignedUrl { get; set; }
}
