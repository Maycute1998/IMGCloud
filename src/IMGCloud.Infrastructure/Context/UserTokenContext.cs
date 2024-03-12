﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Context;

public sealed class TokenContext
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? Token { get; set; }
    public DateTime? ExpireDate { get; set; }
    public bool IsActive { get; set; }
}
