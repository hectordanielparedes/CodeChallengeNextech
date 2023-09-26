using Logic.Models;
using System.Net.Http.Json;
using System.Text.Json;
using StackExchange.Redis;

namespace Logic
{
    public class HackerNewsService: IHackerNewsService
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
            var cachedData = await _cache.StringGet("myCachedDataKey");            

            if (!cachedData.IsNull)
            {
                var deserializedData = JsonSerializer.Deserialize<List<ItemResponse>>(cachedData);
                return deserializedData;
            }
            else
            {
                List<int>? newestStoriesIds = new();
                var newestStories = new List<ItemResponse>();
                var newStoriesResponse = await _httpClientService.GetAsync("v0/newstories.json?print=pretty");
                if (newStoriesResponse != null)
                {
                    newestStoriesIds = newStoriesResponse.Content.ReadFromJsonAsync<List<int>>().Result;
                }

                await Task.WhenAll(newestStoriesIds.Select(async id =>
                {
                    var response = await _httpClientService.GetAsync($"v0/item/{id}.json?print=pretty");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadFromJsonAsync<ItemResponse>();
                        newestStories.Add(content);
                    }
                }));
                var serializedData = JsonSerializer.Serialize(newestStories);
                await _cache.StringSet("myCachedDataKey", serializedData);

                return newestStories;
            }
            
            
        }
    }
}
