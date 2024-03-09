using IMGCloud.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task <ResponeVM> CreatePost(CreatePostRequest post);
        Task <ResponeVM> EditPost(CreatePostRequest post);
        Task <ResponeVM> PressingHeart(CreatePostRequest post);
    }
}
