using StackExchange.Redis;

namespace Logic
{
    public interface ICacheService
    {
        Task<string?> GetStringAsync(string key);
        Task SetStringAsync(string key, string? serializedData);
        Task<RedisValue> StringGet(string key);
        Task<RedisValue> StringSet(string key, string serializedData);
        bool StringDelete(string key);
    }
}
