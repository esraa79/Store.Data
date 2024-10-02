using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.CachService;
using System.Text;

namespace Store.Web.Helper
{
    public class CachAttribute:Attribute,IAsyncActionFilter
    {
        private readonly int _timeToLiveInSeconds;

        public CachAttribute(int timeToLiveInSeconds)
        {
           _timeToLiveInSeconds = timeToLiveInSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cachservice = context.HttpContext.RequestServices.GetRequiredService<ICachService>();
            var cachkey  = GetCacheKeyFromRequest(context.HttpContext.Request);
            var cachResponse = await _cachservice.GetCachResponseAsync(cachkey);
            if(string.IsNullOrEmpty(cachResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachResponse,
                    ContentType = "application/json",
                    StatusCode = 200

                };
                context.Result = contentResult;
                return;
            }
            var executedContext =await next();
            if (executedContext.Result is OkObjectResult response)
            {
                await _cachservice.SetCachResponseAsync(cachkey,response.Value,TimeSpan.FromSeconds(_timeToLiveInSeconds));
            }

        }
        private string GetCacheKeyFromRequest(HttpRequest request)
        {
            StringBuilder cachKey = new StringBuilder();
            cachKey.Append($"{request.Path}");
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
                 cachKey.Append($"{key}-{value}");
            return cachKey.ToString();
          
        }
    }
}
