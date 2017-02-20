using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;

namespace VkNetGine
{
    public delegate void MessageEventHandler(Message msg);

    public class ClockTower
    {
        public event MessageEventHandler Chime;

        public void ChimeIncomingMessage(Message msg)
        {
            Chime(msg);
        }
    }


    public class API
    {
        private HttpClient client;
        private ClockTower _tower;

        public API(string token , ClockTower tower)
        {
            _tower = tower;
            client = new HttpClient();
            RequestBuilder.Token = token;
            // Handle inbox there

            //---   ---
            SetupLP();
        }


        public async void SendMessage(Message m)
        {
            string req = RequestBuilder.SendMsg(m.Target, m.Text);
            await client.GetAsync(req);
        }


        private void DealWithAnswer(string answer)
        {
            JObject o = JObject.Parse(answer);
            RequestBuilder.Lpts = o["ts"].ToString();

            VkEvent e = JsonMituteeDeserializator.ConvertToEvent(answer);
            if (e.HasIncommingMsg())
            {
                foreach(var msg in e.IncommingMessages)
                {
                    _tower.ChimeIncomingMessage(msg);
                }
            }
            CallLP();
        }
        private async void CallLP()
        {
            string req = RequestBuilder.CallLP();
            HttpResponseMessage response = await client.GetAsync(req);
            using (StreamReader sr = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                string answer = sr.ReadToEnd();
                Console.WriteLine(answer);
                DealWithAnswer(answer);
            }
        }
        private async void SetupLP()
        {
            string req = RequestBuilder.InitLP();
            HttpResponseMessage response = await client.GetAsync(req);
            using (StreamReader sr = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                string answer = sr.ReadToEnd();
                Console.WriteLine(answer);

                JObject o = JObject.Parse(answer);
                RequestBuilder.Lpserver = o["response"]["server"].ToString();
                RequestBuilder.Lpkey = o["response"]["key"].ToString();
                RequestBuilder.Lpts = o["response"]["ts"].ToString();
            }
            CallLP();
        }
        
    }
}
