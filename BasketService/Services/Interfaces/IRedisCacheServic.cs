﻿namespace BasketService.Services.Interfaces
{
    public interface IRedisCacheService
    {
        T Get<T>(string key);

        void Set<T>(string key, T value, TimeSpan? expiry = null);

        void Remove(string key);
    }
}
