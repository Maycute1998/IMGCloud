using IMGCloud.Data.Enums;
using Microsoft.AspNetCore.Http;

namespace IMGCloud.Domain.Models
{
    public class CreatePostRequest
    {
        public string? Caption { get; set; }
        public string? Location { get; set; }
        public string? Emotion { get; set; }
        public int? CollectionId { get; set; }
        public PostPrivacy PostPrivacy { get; set; }
        public IFormFile? PostImages { get; set; }
    }
}
