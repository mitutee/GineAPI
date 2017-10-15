using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VkDotNet.Http
{
    public static class HttpService
    {
        public static async Task<string> Get(string Url)
        {
            using (var client = new HttpClient())
                return await client.GetStringAsync(Url);
        }
    }
}
