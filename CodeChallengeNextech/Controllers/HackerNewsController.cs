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
        public async Task<IActionResult> GetNewestStories()
        {
            try
            {
                var resp = await _hackerNewsService.GetNewestStories();
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
