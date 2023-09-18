using CodeChallengeNextech.Controllers;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Test.Controller
{
    public class HackerNewsControllerTest
    {
        [Fact]
        public void HackerNewsController_Should_ReturnBadRequestException()
        {
            // Arrange
            Mock<IHackerNewsService> mockHackerNewsService = new();
            mockHackerNewsService.Setup(mock => mock.GetNewestStories()).ThrowsAsync(new Exception("Test exception"));

            HackerNewsController hackerNewsController = new(mockHackerNewsService.Object);
            var response = hackerNewsController.GetNewestStories();

            var badRequestResponse = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Contains("Test exception", badRequestResponse.Value.ToString());
        }
    }
}
