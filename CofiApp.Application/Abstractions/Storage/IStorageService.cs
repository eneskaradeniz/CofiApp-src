using CofiApp.Domain.Enums;

namespace CofiApp.Application.Abstractions.Storage
{
    public interface IStorageService : IStorage
    {
        public StorageType StorageType { get; }
    }
}
