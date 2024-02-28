using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Data.Entities
{
    public class Collection: BaseEntity
    {
        public int UserId { get; set; }
        public string? CollectionName { get; set; }
        public string? Description { get; set; }

    }
}
