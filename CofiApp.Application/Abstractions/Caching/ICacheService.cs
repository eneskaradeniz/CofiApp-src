using Microsoft.Extensions.Caching.Distributed;

namespace CofiApp.Application.Abstractions.Caching
{
    public interface ICacheService
    {
        Task<T?> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, DistributedCacheEntryOptions? options = null, CancellationToken cancellationToken = default);
    }
}
