using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace vk_dotnet.Methods
{
    public class Method
    {
        private static string _mainURI = "https://api.vk.com/method";        

        public static async Task<string> SendGetAsync (string request_uri)
        {
            using (var cl = new HttpClient())
            {
                var res = await cl.GetAsync(request_uri);
                return await res.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> SendPostAsync(string request_uri, string login, string password)
        {
            
            using (var cl = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("act", "login"),
                    new KeyValuePair<string, string>("al_frame", "al_frame"),
                    new KeyValuePair<string, string>("_origin", "https://vk.com"),
                    new KeyValuePair<string, string>("utf8", "1"),
                    new KeyValuePair<string, string>("email", login),
                    new KeyValuePair<string, string>("pass", password),
                    new KeyValuePair<string, string>("lg_h", login),
                });
                var res = await cl.PostAsync(request_uri,content);
                return await res.Content.ReadAsStringAsync();
            }
        }


        private string _token;

        public Method(string token)
        {
            _token = token;
        }

        public string GetMethodUri(string method, params string[] parameters)
        {

            string prms = "";
            for (int i = 0; i < parameters.Length; i++)
            {
                prms += parameters[i] + '&';
            }

            string request_uri = $"{_mainURI}/{method}?{prms}access_token={_token}&v=5.62";
            return request_uri;


        }
    }
}
