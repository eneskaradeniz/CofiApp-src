using CofiApp.Contracts.Files;

namespace CofiApp.Application.Abstractions.Storage
{
    public interface IBlobService
    {
        Task<Guid> UploadAsync(Stream stream, string contetType, string containerName, CancellationToken cancellationToken = default);
        Task<FileResponse> DownloadAsync(Guid fileId, string containerName, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid fileId, string containerName, CancellationToken cancellationToken = default);
    }
}
