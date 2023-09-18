using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IHackerNewsService
    {
        Task<List<ItemResponse>> GetNewestStories();
    }
}
