using CodeChallengeNextech.Controllers;
using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Test.Controller
{
    public class HackerNewsControllerTest
    {
        [Fact]
        public async Task HackerNewsController_Should_ReturnCorrectValue()
        {
            // Arrange
            var expectedStories = new List<ItemResponse>
            {
                new() { title = "Test Story 1", url = "https://test.example.com"}
            };

            Mock<IHackerNewsService> mockHackerNewsService = new();
            mockHackerNewsService.Setup(mock => mock.GetNewestStories()).ReturnsAsync(expectedStories);

            HackerNewsController hackerNewsController = new(mockHackerNewsService.Object);

            // Act
            var response = await hackerNewsController.GetNewestStories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response);
            var actualStories = Assert.IsAssignableFrom<IEnumerable<ItemResponse>>(okResult.Value);
            Assert.Equal(expectedStories.Count, actualStories.Count());
        }

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
