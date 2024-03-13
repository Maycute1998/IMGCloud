using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Context;

public sealed class SignInContext
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}
