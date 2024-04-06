using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using IMGCloud.Domain.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Infrastructure.Services
{
    public class GoogleCloudService : IGoogleCloudService
    {
        private readonly GoogleCredential _googleCredential;
        private readonly StorageClient _storageClient;
        private readonly GoogleCloudOptions _googleCloudOptions;

        public GoogleCloudService(IConfiguration configuration, GoogleCloudOptions googleCloudOptions)
        {
            _googleCredential = GoogleCredential.FromFile(configuration["GoogleCredential"]);
            _storageClient = StorageClient.Create(_googleCredential);
            _googleCloudOptions = googleCloudOptions;
        }

        public Task DeleteFileAsync(string fileNameForStorage, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<string> UploadFileAsync(string base64String, CancellationToken cancellationToken)
        {
            var bucketName = _googleCloudOptions.BucketName;
            var folderContained = $"{_googleCloudOptions.Prefix.Photos}";

            base64String = base64String.Replace("data:image/png;base64,", "");
            byte[] bytes = Convert.FromBase64String(base64String);

            string fileName = Guid.NewGuid().ToString();
            using var ms = new MemoryStream(bytes);
            await _storageClient.UploadObjectAsync(bucketName, fileName, "image/jpeg", ms);
            return $"https://storage.googleapis.com/{bucketName}/{folderContained}/{fileName}";
        }
    }
}
