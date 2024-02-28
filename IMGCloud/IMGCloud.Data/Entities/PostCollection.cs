using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Data.Entities
{
    public class PostCollection : BaseEntity
    {
        public int PostId { get; set; }
        public int CollectionName { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
