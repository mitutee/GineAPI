using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using vk_dotnet.Objects;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

            using (var cl = new HttpClient())
            {
                //cl.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                //cl.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                //cl.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36");
                //cl.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");
                
                Console.WriteLine("Headers have been set.");
                HttpResponseMessage response = await cl.GetAsync("http://vk.com/");
                string response_text = await response.Content.ReadAsStringAsync();

                Console.WriteLine(response_text);

                string lg_h = extractWithGivenPattern(REGEX_LOGIN_HASH, response_text);
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