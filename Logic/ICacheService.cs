using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ICacheService
    {
        Task<string?> GetStringAsync(string key);
        Task SetStringAsync(string key, string? serializedData);
        
    }
}
