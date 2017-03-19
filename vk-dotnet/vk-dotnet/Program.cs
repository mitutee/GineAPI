using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using vk_dotnet.Objects;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;

namespace vk_dotnet
{
    class Program
    {
        public static string t = "b22bbc07a3602aa6dae05ab3fefa7ad0b72a9349ad4cd6bcc0e268772ec83d2e0fdac1a9f6325f83c7100";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Auth("+380505102671", "oJolgolo243VK");

            Console.ReadKey();
        }
        //\"lg_h\" value=\"([a - z0 - 9] +)\"
        public static async Task<string> Auth(string login, string password)
        {
            string REGEX_LOGIN_HASH = @"lg_h=(\d+\w+)";

            Dictionary<string, string> data_for_login = new Dictionary<string, string>();

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) )
            {
                ///usually i make a standard request without authentication, eg: to the home page.
                ///by doing this request you store some initial cookie values,
                ///that might be used in the subsequent login request and checked by the server
                HttpResponseMessage response = await client.GetAsync("http://vk.com/");                
                string response_text = await response.Content.ReadAsStringAsync();
                //Extracted login hash for sending post requset
                string lg_h = extractWithGivenPattern(REGEX_LOGIN_HASH, response_text);
                //Form infrormation
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("act", "login"),
                    new KeyValuePair<string, string>("al_frame", "al_frame"),
                    new KeyValuePair<string, string>("_origin", "https://vk.com"),
                    new KeyValuePair<string, string>("utf8", "1"),
                    new KeyValuePair<string, string>("email", login),
                    new KeyValuePair<string, string>("pass", password),
                    new KeyValuePair<string, string>("lg_h", lg_h),
                });

                var r = await client.PostAsync("https://login.vk.com/", content);

                var cookies_col = cookieContainer.GetCookies(new Uri("https://login.vk.com/"));
                data_for_login.Add("p", cookies_col["p"].Value);
                data_for_login.Add("l", cookies_col["l"].Value);
                data_for_login.Add("remixsid", cookies_col["remixsid"].Value);

                var resp = await client.GetAsync("https://vk.com/friends");
                
                Console.WriteLine(await resp.Content.ReadAsStringAsync());
                foreach (var d in data_for_login)
                {
                    Console.WriteLine($"{d.Key} -- {d.Value}");
                }

                return "";
            }
        }

        private static string extractWithGivenPattern(string pattern, string text)
        {
            Regex p = new Regex(pattern);
            Match match = p.Match(text);
            return match.Groups[1].Value;
        }


    }
}