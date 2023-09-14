using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallengeNextech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly HackerNewsService _hackerNewsService;

        public HackerNewsController(HackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        [HttpGet]
        public async Task<List<ItemResponse>> GetNewestStories()
        {
            var resp = await _hackerNewsService.GetNewestStories();
            return resp;
        }

    }
}
