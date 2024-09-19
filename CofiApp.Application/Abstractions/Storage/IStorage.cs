using CofiApp.Contracts.Files;
using CofiApp.Domain.Enums;

namespace CofiApp.Application.Abstractions.Storage
{
    public interface IStorage
    {
        StorageType StorageType { get; }
        Task<Guid> UploadAsync(Stream stream, string contentType, string path, CancellationToken cancellationToken = default);
        Task<FileResponse> DownloadAsync(Guid fileId, string path, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid fileId, string path, CancellationToken cancellationToken = default);
    }
}
