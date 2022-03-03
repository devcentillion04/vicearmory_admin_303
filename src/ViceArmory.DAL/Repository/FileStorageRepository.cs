using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using ViceArmory.DAL.Repository.Interface;

namespace ViceArmory.DAL.Repository
{
    public class FileStorageRepository : IFileStorageRepository
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        public FileStorageRepository(IAmazonS3 s3Client, string bucketName)
        {
            _s3Client = s3Client;
            _bucketName = bucketName;
        }
        public async Task<string> SaveAsync(
            string containerName,
            string blobName,
            Stream source,
            string fileNameSuffix,
            bool closeStream = true,
            bool isPublic = false)
        {
            var putObjRequest = new PutObjectRequest();
            putObjRequest.BucketName = _bucketName;
            putObjRequest.InputStream = source;
            putObjRequest.AutoCloseStream = closeStream;
            fileNameSuffix = string.IsNullOrWhiteSpace(fileNameSuffix) ? string.Empty : $"_{fileNameSuffix}";
            var uniqueFileName = $"{GetUniqueFileName()}{fileNameSuffix}{Path.GetExtension(blobName)}";
            putObjRequest.Key = Path.Combine(containerName, uniqueFileName);

            if (isPublic)
            {
                putObjRequest.CannedACL = S3CannedACL.PublicRead;
            }

            await _s3Client.PutObjectAsync(putObjRequest);

            return uniqueFileName;
        }

        public async Task<Stream> GetStreamAsync(string key)
        {
            var getObjResponse = await _s3Client.GetObjectAsync(_bucketName, key);
            return getObjResponse.ResponseStream;
        }
        private string GetUniqueFileName()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString("X2");
        }
    }
}
