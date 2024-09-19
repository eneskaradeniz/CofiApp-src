using CofiApp.Application.Abstractions.Storage;
using CofiApp.Contracts.Files;
using CofiApp.Domain.Enums;

namespace CofiApp.Infrastructure.Storage
{
    internal sealed class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public StorageType StorageType { get => _storage.StorageType; }

        public async Task<Guid> UploadAsync(Stream stream, string contentType, string path, CancellationToken cancellationToken = default) 
            => await _storage.UploadAsync(stream, contentType, path, cancellationToken);

        public async Task<FileResponse> DownloadAsync(Guid fileId, string path, CancellationToken cancellationToken = default)
            => await _storage.DownloadAsync(fileId, path, cancellationToken);

        public async Task DeleteAsync(Guid fileId, string path, CancellationToken cancellationToken = default)
            => await _storage.DeleteAsync(fileId, path, cancellationToken);
    }
}
