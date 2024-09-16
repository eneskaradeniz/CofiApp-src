using CofiApp.Application.Abstractions.Caching;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CofiApp.Infrastructure.Caching
{
    internal class CacheService : ICacheService
    {
        private readonly DistributedCacheEntryOptions Default = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        };

        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetOrCreateAsync<T>(
            string key,
            Func<CancellationToken, Task<T>> factory,
            DistributedCacheEntryOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);

            T? value;
            if (!string.IsNullOrWhiteSpace(cachedValue))
            {
                value = JsonSerializer.Deserialize<T>(cachedValue);

                if (value is not null)
                {
                    return value;
                }
            }

            value = await factory(cancellationToken);

            if (value is null)
            {
                return default;
            }

            await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), options ?? Default, cancellationToken);

            return value;
        }
    }
}
