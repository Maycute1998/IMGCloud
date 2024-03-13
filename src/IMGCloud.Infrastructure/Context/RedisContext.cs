using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Context;

public sealed class RedisContext
{
    public string? RedisKey { get; set; }
    public string? Token { get; set; }
    public DateTime? ExpiredDate { get; set; }
}
