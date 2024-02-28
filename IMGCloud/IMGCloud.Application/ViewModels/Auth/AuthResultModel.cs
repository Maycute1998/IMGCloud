﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Application.ViewModels.Auth
{
    public class AuthResultModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    public class TokenResult : AuthResultModel
    {
        public string Token { get; set; }
    }

    public class RegisterResult : AuthResultModel
    {
        public List<RegisterErrorResult> Errors { get; set; } = new List<RegisterErrorResult>();
    }

    public class RegisterErrorResult
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class ResponeVM
    {
        public bool Status { get; set; }

        public string? Message { get; set; }
    }
}
