using IMGCloud.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace IMGCloud.Infrastructure.Requests;

public record CreatePostRequest(string? Caption, string? Location, string? Emotion, int? CollectionId, PostPrivacy PostPrivacy, string ImagePath)
{

}

