﻿using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace CorectMyQuran.Application.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static async Task SetRecordAsync<T>(
            this IDistributedCache cache,
            string recordId,
            T data,
            TimeSpan? absoluteExpireTime = null,
            TimeSpan? unusedExpireTime = null
        )
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow =
                absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime;

            var jsonData = JsonSerializer.Serialize(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T?> GetRecordAsync<T>(
            this IDistributedCache cache,
            string recordId
        )
        {
            var jsonData = await cache.GetStringAsync(recordId);

            return jsonData is null ? default(T) : JsonSerializer.Deserialize<T>(jsonData);
        }
    }
}
