using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vk_dotnet.Objects;

namespace vk_dotnet.Methods
{
    public class LongPoll_Methods : Method
    {
        public LongPoll_Methods(string token) : base(token)
        {
        }

        public string _lpserver { get; private set; }
        public string _lpkey { get; private set; }
        public string _lpts { get; private set; }

        public async Task GetLongPollServer()
        {
            establish_LP:
            string request = ApiMethods.GetMethodUri("messages.getLongPollServer",
                "use_ssl=1",
                "need_pts=1",
                "v=5.56",
                $"access_token={_token}"
                );
            try
            {
                string response = await SendGetAsync(request);

                JObject o = JObject.Parse(response);
                _lpserver = o["response"]["server"].ToString();
                _lpkey = o["response"]["key"].ToString();
                _lpts = o["response"]["ts"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                goto establish_LP;
            }



        }

        public async Task<List<List<string>>> CallLongPoll()
        {
            string request = $"https://{_lpserver}?act=a_check&key={_lpkey}&ts={_lpts}&wait=25&mode=128&version=1";

            string response = await SendGetAsync(request);
            JObject o = JObject.Parse(response);
            _lpts = o["ts"].ToString();

            List<List<string>> updates = JsonConvert.DeserializeObject<List<List<string>>>(o["updates"].ToString()); 

            updates.ForEach((e) => e.ForEach((el) => Console.WriteLine(el)));

            return updates;
        }
    }
}
