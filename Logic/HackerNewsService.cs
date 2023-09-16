using Logic.Models;
using System.Net.Http.Json;
using System.Text.Json;
using StackExchange.Redis;

namespace Logic
{
    public class HackerNewsService
    {
        private readonly ICacheService _cache;
        private readonly IHttpClientService _httpClientService;

        public HackerNewsService(ICacheService cache, IHttpClientService httpClientService)   
        {
            _cache = cache;
            _httpClientService = httpClientService;
        }

        public async Task<List<ItemResponse>> GetNewestStories()
        {
            RedisValue cachedData = await _cache.StringGet("myCachedDataKey");            

            if (!cachedData.IsNull)
            {
                var deserializedData = JsonSerializer.Deserialize<List<ItemResponse>>(cachedData);
                return deserializedData;
            }
            else
            {
                List<int>? newestStoriesIds = new();
                var newestStories = new List<ItemResponse>();
                var newStoriesResponse = await _httpClientService.GetAsync("v0/newstories.json?print=pretty&orderBy=\"$priority\"&limitToFirst=40");
                if (newStoriesResponse != null)
                {
                    newestStoriesIds = newStoriesResponse.Content.ReadFromJsonAsync<List<int>>().Result;
                }               

                foreach (var id in newestStoriesIds)
                {
                    var itemResponse = await _httpClientService.GetAsync($"v0/item/{id}.json?print=pretty");
                    if (itemResponse != null)
                    {
                        var item = itemResponse.Content.ReadFromJsonAsync<ItemResponse>().Result;
                        newestStories.Add(item);
                    }
                    
                }

                var serializedData = JsonSerializer.Serialize(newestStories);
                await _cache.StringSet("myCachedDataKey", serializedData);

                return newestStories;
            }
            
            
        }
    }
}
