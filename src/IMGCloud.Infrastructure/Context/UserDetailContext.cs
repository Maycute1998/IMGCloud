using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Context
{
    public class UserDetailContext
    {
        public int UserId { get; set; }
        public string? UserName { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Photo { get; set; }
        public string? Address { get; set; }
        public string? Birthday { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
    }
    
}
