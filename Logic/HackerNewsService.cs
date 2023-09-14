using Logic.Models;
using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Logic
{
    public class HackerNewsService
    {
        private readonly IDistributedCache _cache;
        public HackerNewsService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<List<ItemResponse>> GetNewestStories()
        {
            var cachedData = await _cache.GetStringAsync("myCachedDataKey");
            if (cachedData != null)
            {
                var deserializedData = JsonSerializer.Deserialize<List<ItemResponse>>(cachedData);
                return deserializedData;
            }
            else
            {
                var newestStories = new List<ItemResponse>();
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://hacker-news.firebaseio.com");
                var newestStoriesIds = await client.GetFromJsonAsync<List<int>>("v0/newstories.json?print=pretty&orderBy=\"$priority\"&limitToFirst=40");                

                foreach (var id in newestStoriesIds)
                {
                    var item = await client.GetFromJsonAsync<ItemResponse>($"v0/item/{id}.json?print=pretty");
                    newestStories.Add(item);
                }

                var serializedData = JsonSerializer.Serialize(newestStories);
                await _cache.SetStringAsync("myCachedDataKey", serializedData, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

                return newestStories;
            }
            
            
        }
    }
}
