using IMGCloud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Context
{
    public class PostContext
    {
        public int Id { get; set; }
        public string? Caption { get; set; }
        public string? Location { get; set; }
        public string? Emotion { get; set; }
        public int? CollectionId { get; set; }
        public PostPrivacy PostPrivacy { get; set; }
        public string? ImagePath { get; set; }
        public string? UserName { get; set; }
        public string? UserAvatar { get; set; }
    }
}
