using Logic;
using Moq;
using StackExchange.Redis;
using System.Net;
using Xunit;

namespace Test.Logic
{
    public class HackerNewsServiceTest
    {
        [Fact]
        public void GetNewestStories_Should_StringGetReturnsNullResponse()
        {
            // Arrange
            Mock<ICacheService> mockDistributedCache = new();
            HttpClient httpClient = new();
            Mock<IHttpClientService> mockHttpClientService = new();

            mockDistributedCache.Setup(mock => mock.StringGet("myCachedDataKey").Result).Returns<Task<string?>>(null);
            mockHttpClientService.Setup(mock => mock.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[]")
            });
            
            HackerNewsService hackerNewsService = new(mockDistributedCache.Object, mockHttpClientService.Object);

            // Act
            var response = hackerNewsService.GetNewestStories();

            // Assert
            mockDistributedCache.Verify(mock => mock.StringGet("myCachedDataKey"), Times.Once);
        }

        [Fact]
        public void GetNewestStories_Should_StringGetReturnsResponse()
        {
            // Arrange
            Mock<ICacheService> mockDistributedCache = new();
            HttpClient httpClient = new();
            Mock<IHttpClientService> mockHttpClientService = new();
            mockDistributedCache.Setup(mock => mock.StringGet("myCachedDataKey").Result).Returns(new RedisValue());
            mockHttpClientService.Setup(mock => mock.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("[]")
            });

            HackerNewsService hackerNewsService = new(mockDistributedCache.Object, mockHttpClientService.Object);

            // Act
            var response = hackerNewsService.GetNewestStories();
            // Assert
            mockDistributedCache.Verify(mock => mock.StringGet("myCachedDataKey"), Times.Once);
            mockDistributedCache.Verify(mock => mock.StringSet("myCachedDataKey", It.IsAny<string>()), Times.Once);
        }
    }
}
