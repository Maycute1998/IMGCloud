using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Services
{
    public interface IGoogleCloudService
    {
        Task<string> UploadFileAsync(string base64String, CancellationToken cancellationToken = default);
        Task DeleteFileAsync(string fileNameForStorage, CancellationToken cancellationToken = default);
    }
}
