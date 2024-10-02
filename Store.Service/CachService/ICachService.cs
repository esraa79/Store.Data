using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.CachService
{
    public interface ICachService
    {
        Task SetCachResponseAsync(string key, object response, TimeSpan timeToLive);
        Task<string> GetCachResponseAsync(string key);
    }
}
