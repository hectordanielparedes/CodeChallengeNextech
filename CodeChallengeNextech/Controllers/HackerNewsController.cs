using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallengeNextech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        public HackerNewsController(IHackerNewsService hackerNewsService)
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
