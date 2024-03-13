using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Options;

public sealed class GoogleAuthenticationOptions
{
    public string ClientId { get; set; } = string.Empty;
}
