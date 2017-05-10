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

        /// <summary>
        /// Gets session data for Long Poll server and saves is.
        /// </summary>
        /// <returns></returns>
        public async Task GetLongPollServer()
        {
            establish_LP:
            string request = GetMethodUri("messages.getLongPollServer",
                "use_ssl=1",
                "need_pts=1",
                $"access_token={_token}"
                );
            try {
                string response = await CallApiAsync(request);
                var longPoll = JsonConvert.DeserializeObject<GetLongPollResponse>(response);
                _lpserver = longPoll.server;
                _lpkey = longPoll.key;
                _lpts = longPoll.ts;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                goto establish_LP;
            }
        }



        public async Task<List<List<string>>> CallLongPoll()
        {
            call_LP:
            string request = $"https://{_lpserver}?act=a_check&key={_lpkey}&ts={_lpts}&wait=25&mode=128&version=1";

            string response = await SendGetAsync(request);

            Console.WriteLine($"{DateTime.Now} --- {response}");
            CallLongPollResponse longPoll;
            try
            {
                longPoll = JsonConvert.DeserializeObject<CallLongPollResponse>(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($"EXCEPTION --- {DateTime.Now} --- {e.Message}");
                await GetLongPollServer();
                goto call_LP;
            }

            if (longPoll.Failed != null || longPoll.ts == null) {
                Console.WriteLine($"@ERR --- {DateTime.Now} --- {longPoll.Failed} --- {response}");
                await GetLongPollServer();
                goto call_LP;
            }
            else {
                _lpts = longPoll.ts;
            }
            return longPoll.Updates;
        }

        public static List<Message> ParseEventForMessages(List<List<string>> updates)
        {
            List<Message> messages = new List<Message>();
            foreach (var ev in updates) {
                if (hasIncomeMessage(ev)) {
                    Message m = parseMessage(ev);
                    messages.Add(m);
                }
            }
            return messages;
        }

        private static Message parseMessage(List<string> ev)
        {
            Message message = new Message();
            message.Id = Int32.Parse(ev[1]);
            message.Peer_id = ev[3];// Console.WriteLine("ID IS : "+ev[3]);
            message.Body = ev[6];
            return message;
        }

        private static bool hasIncomeMessage(List<string> ev) => (Int32.Parse(ev[0]) == 4) && ((Int32.Parse(ev[2]) & 2) == 0);

    }
}
