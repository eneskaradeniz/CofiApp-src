using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CofiApp.Application.Abstractions.Storage;
using CofiApp.Contracts.Files;
using CofiApp.Domain.Enums;

namespace CofiApp.Infrastructure.Storage
{
    internal sealed class BlobStorage(BlobServiceClient blobServiceClient) : IBlobStorage
    {
        public StorageType StorageType => StorageType.Blob;

        public async Task<Guid> UploadAsync(Stream stream, string contentType, string path, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(path);
            await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

            Guid fileId = Guid.NewGuid();
            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.UploadAsync(
                stream,
                new BlobHttpHeaders { ContentType = contentType },
                cancellationToken: cancellationToken);

            return fileId;
        }

        public async Task<FileResponse> DownloadAsync(Guid fileId, string path, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(path);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            Response<BlobDownloadResult> response = await blobClient.DownloadContentAsync(cancellationToken: cancellationToken);

            return new FileResponse(response.Value.Content.ToStream(), response.Value.Details.ContentType);
        }

        public async Task DeleteAsync(Guid fileId, string path, CancellationToken cancellationToken = default)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(path);

            BlobClient blobClient = containerClient.GetBlobClient(fileId.ToString());

            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }
    }
}
