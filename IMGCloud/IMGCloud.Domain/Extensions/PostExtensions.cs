using IMGCloud.Data.Entities;
using IMGCloud.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Extensions;

public static class PostExtensions
{
    public static Post ToEntity(this CreatePostRequest request)
    => new()
    {
        Caption = request.Caption,
        Location = request.Location,
        Emotion = request.Emotion,
        PostPrivacy = request.PostPrivacy,

    };
}
