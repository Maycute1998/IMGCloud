using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Data.Entities
{
    public class PostImage : BaseEntity
    {
        public int PostId { get; set; }
        public string? ImagePath { get; set; }
        public string? Caption { get; set; }
        public long FileSize { get; set; }
        public Post? Post { get; set; }
    }
}
