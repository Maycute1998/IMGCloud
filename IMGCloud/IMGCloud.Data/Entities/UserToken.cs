using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Data.Entities
{
    public class UserToken : BaseEntity
    {
        public int? UserId { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpireDays { get; set; }
        public User? User { get; set; }
    }
}
