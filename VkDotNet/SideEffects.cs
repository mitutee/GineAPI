using System;
using System.Collections.Generic;
using System.Linq;
using VkDotNet.Core;
using VkDotNet.Core.QueryBuilders;
using VkDotNet.Http;

namespace VkDotNet
{
    public static class SideEffects
    {
        const string BaseUrl = "https://api.vk.com/method";
        public static string Execute(this ApiQuery q)
        {
            string requestUrl = $"{BaseUrl}/{q.MethodModule}.{q.MethodName}" + q.Params.ToQueryString();
            return HttpService.Get(requestUrl).Result;
        }

        public static string ToQueryString(this IDictionary<string, string> d)
        {
            var queries = d.Select(kvp => $"{kvp.Key}={kvp.Value}");
            return "?" + String.Join("&", queries);
        }       
    }
}
