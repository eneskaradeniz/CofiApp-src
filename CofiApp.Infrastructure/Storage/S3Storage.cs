using Amazon.S3;
using Amazon.S3.Model;
using CofiApp.Application.Abstractions.Storage;
using CofiApp.Contracts.Files;
using CofiApp.Domain.Enums;
using System.Net;

namespace CofiApp.Infrastructure.Storage
{
    internal sealed class S3Storage(IAmazonS3 s3Client) : IS3Storage
    {
        public StorageType StorageType => StorageType.S3;
        public async Task<Guid> UploadAsync(Stream stream, string contentType, string path, CancellationToken cancellationToken = default)
        {
            Guid fileId = Guid.NewGuid();

            if (!await BucketExistsAsync(path, cancellationToken))
            {
                var response = await s3Client.PutBucketAsync(path, cancellationToken);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Bucket could not be created.");
                }
            }

            PutObjectRequest putRequest = new()
            {
                BucketName = path,
                Key = fileId.ToString(),
                InputStream = stream,
                ContentType = contentType,
                CannedACL = S3CannedACL.PublicRead
            };

            await s3Client.PutObjectAsync(putRequest, cancellationToken);

            return fileId;
        }

        public async Task<FileResponse> DownloadAsync(Guid fileId, string path, CancellationToken cancellationToken = default)
        {
            GetObjectRequest getRequest = new()
            {
                BucketName = path,
                Key = fileId.ToString()
            };

            using GetObjectResponse response = await s3Client.GetObjectAsync(getRequest, cancellationToken);

            return new FileResponse(
                Stream: response.ResponseStream,
                ContentType: response.Headers.ContentType
            );
        }

        public async Task DeleteAsync(Guid fileId, string path, CancellationToken cancellationToken = default)
        {
            DeleteObjectRequest deleteRequest = new()
            {
                BucketName = path,
                Key = fileId.ToString()
            };

            await s3Client.DeleteObjectAsync(deleteRequest, cancellationToken);
        }

        private async Task<bool> BucketExistsAsync(string bucketName, CancellationToken cancellationToken = default)
        {
            ListBucketsResponse response = await s3Client.ListBucketsAsync(cancellationToken);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Could not list buckets.");
            }

            return response.Buckets.Any(b => b.BucketName == bucketName);
        }
    }
}
