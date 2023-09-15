using Logic.Models;
using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Logic
{
    public class HackerNewsService
    {
        private readonly ICacheService _cache;
        private readonly HttpClientService _httpClientService;

        public HackerNewsService(ICacheService cache, HttpClientService httpClientService)   
        {
            _cache = cache;
            _httpClientService = httpClientService;
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
                await _cache.SetStringAsync("myCachedDataKey", serializedData);

                return newestStories;
            }
            
            
        }
    }
}
