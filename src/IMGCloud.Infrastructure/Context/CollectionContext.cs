using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Context
{
    public class CollectionContext
    {
        public int Id { get; set; }
        public string CollectionName { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
    }
}
