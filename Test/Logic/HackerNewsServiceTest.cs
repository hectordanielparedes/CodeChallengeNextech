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
            Mock<IHttpClientService> mockHttpClientService = new();

            

            mockDistributedCache.Setup(mock => mock.GetStringAsync(It.IsAny<string>()).Result).Returns<Task<string?>>(null);
            mockHttpClientService.Setup(mock => mock.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[]")
            });

            // Arrange
            HackerNewsService hackerNewsService = new(mockDistributedCache.Object, mockHttpClientService.Object);

            // Act
            var response = hackerNewsService.GetNewestStories();

            // Assert
            mockDistributedCache.Verify(mock => mock.GetStringAsync("myCachedDataKey"), Times.Once);
        }
    }
}
