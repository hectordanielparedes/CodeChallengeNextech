using Logic;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Net;
using Xunit;

namespace Test.Logic
{
    public class HackerNewsServiceTest
    {
        [Fact]
        public void GetNewestStories_Should_ReturnsResponse()
        {

            Mock<ICacheService> mockDistributedCache = new();
            HttpClient httpClient = new();
            Mock<HttpClientService> mockHttpClientService = new(httpClient);

            

            //mockDistributedCache.Setup(x => x.GetStringAsync(It.IsAny<string>()));

            mockDistributedCache.Setup(mock => mock.GetStringAsync(It.IsAny<string>()).Result).Returns<Task<string?>>(null);
            //mockDistributedCache.Setup(mock => mock.GetStringAsync("myCachedDataKey", CancellationToken.None)).ReturnsAsync<Task<string?>>(null));

            // Arrange
            HackerNewsService hackerNewsService = new(mockDistributedCache.Object, mockHttpClientService.Object);

            // Act
            var response = hackerNewsService.GetNewestStories();

            // Assert

        }
    }
}
