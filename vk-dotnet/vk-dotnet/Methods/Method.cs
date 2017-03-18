using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace vk_dotnet.Methods
{
    public class Method
    {
        public static async Task<string> SendRequestAsync (string request_uri)
        {
            using (var cl = new HttpClient())
            {
                var res = await cl.GetAsync(request_uri);
                return await res.Content.ReadAsStringAsync();
            }
        }
    }
}
