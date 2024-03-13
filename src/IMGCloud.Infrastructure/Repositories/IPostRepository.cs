using IMGCloud.Infrastructure.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Repositories;

public interface IPostRepository
{
    Task CreatePostAsync(CreatePostRequest post, CancellationToken cancellationToken = default);
    Task EditPostAsync(CreatePostRequest post, CancellationToken cancellationToken = default);
    Task PressHeartAsync(CreatePostRequest post, CancellationToken cancellationToken = default);
}
