using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
