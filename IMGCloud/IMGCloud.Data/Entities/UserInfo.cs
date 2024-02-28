using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Data.Entities
{
    public class UserInfo : BaseEntity
    {
        public int UserId { get; set; }

        public string? PhoneNumber { get; set; }

        public string? FullName { get; set; }

        public string? Photo { get; set; }

        public string? Bio { get; set; }
        public string? Link { get; set; }
        public string? Friend { get; set; }

        public string? Url { get; set; }

        public DateTime? BirthDay { get; set; }

        public string? Address { get; set; }

        public User? User { get; set; }
    }
}
