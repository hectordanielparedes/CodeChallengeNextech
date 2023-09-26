using Logic.Models;

namespace Logic
{
    public interface IHackerNewsService
    {
        Task<List<ItemResponse>> GetNewestStories();
    }
}
