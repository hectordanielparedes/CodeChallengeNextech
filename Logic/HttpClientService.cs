namespace Logic
{
    public class HttpClientService: IHttpClientService
    {
        private readonly IHttpClientFactory _factory;

        public HttpClientService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            var client = _factory.CreateClient("hackernews");
            return await client.GetAsync(url);
        }
    }
}
