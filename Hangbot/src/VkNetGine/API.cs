using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
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

    /// <summary>
    /// Tools for interaction between bot / console / GUI and VS server
    /// </summary>
    public class API
    {
        private HttpClient client; // For sending requests and getting responses
        private ClockTower _tower; // {Handles events ( incoming messages ) and chimes bot about each}

        //********************************* CONSTRUCTORS *********************************************

        /// <summary>
        /// Creates new instance of API class and starts interaction with server ( <see cref="SetupLP"/> )
        /// </summary>
        /// <param name="token">Access token for your account / group </param>
        /// <param name="tower">{Event handler}</param>
        public API(string token , ClockTower tower)
        {
            _tower = tower;
            client = new HttpClient();
            RequestBuilder.Token = token;
            // Handle unanswered inbox messages here
            HandleUnreadDialogs();
            //---   ---
            SetupLP(); // Start our work
        }
       

        //********************************* PUBLIC METHODS *******************************************

        /// <summary>
        /// Asynchronously sends message
        /// </summary>
        /// <param name="m">Message to send ( contains target )</param>
        public async void SendMessage(Message m)
        {
            string req = RequestBuilder.SendMsg(m.Target, m.Text);
            await client.GetAsync(req);
        }

        //********************************************************************************************

        /// <summary>
        /// 1.Gets answer in JSON string
        /// 2.Converts it to <see cref="VkEvent"/>
        /// 3.Sends event to <see cref="HandleIncomingMessages(VkEvent)"/>
        /// </summary>
        /// <param name="answer"></param>
        private void DealWithAnswer(string answer)
        {
            //**
            VkEvent e = JsonMituteeDeserializator.ConvertToEvent(answer); // TO REFACTORING
            //**
            HandleIncomingMessages(e); // Check event for incoming messages
        }

        /// <summary>
        /// Checks event for incoming messages and if they are there : 
        /// Notifies bots via <see cref="_tower"/>
        /// </summary>
        /// <param name="e"></param>
        private void HandleIncomingMessages(VkEvent e)
        {

            if (e.HasIncommingMsg())
            {
                foreach (var msg in e.IncommingMessages)
                {
                    _tower.ChimeIncomingMessage(msg);
                }
            }
        }
        
        /// <summary>
        /// Calls Long Poll server, waits for answer and process it;
        /// Resets <see cref="RequestBuilder"/> "ts" property;
        /// Recursively sends next request to start listening for next update;
        /// </summary>
        private async void CallLP()
        {
            string req = RequestBuilder.CallLP();
            HttpResponseMessage response = await client.GetAsync(req);
            using (StreamReader sr = new StreamReader(await response.Content.ReadAsStreamAsync()))
            {
                string answer = sr.ReadToEnd();
              //  Console.WriteLine(answer);
                DealWithAnswer(answer);

                JObject o = JObject.Parse(answer);
                RequestBuilder.Lpts = o["ts"].ToString();
                CallLP();
            }
        }
        /// <summary>
        /// Sends first request to Long Poll Server, setups RequestBuilder properties : server, key and ts
        /// Calls <see cref="CallLP()"/> to get next notification about event
        /// </summary>
        private async void SetupLP()
        {
             establis_LP:
            try {

               
                string req = RequestBuilder.InitLP();
                HttpResponseMessage response = await client.GetAsync(req);
                using (StreamReader sr = new StreamReader(await response.Content.ReadAsStreamAsync())) {
                    string answer = sr.ReadToEnd();
                    Console.WriteLine("Connection with Long Poll Server establised at {0}", DateTime.Now);
                    JObject o = JObject.Parse(answer);
                    //--  Setup Request builder class for sending requests to Long Poll Server --
                    RequestBuilder.Lpserver = o["response"]["server"].ToString();
                    RequestBuilder.Lpkey = o["response"]["key"].ToString();
                    RequestBuilder.Lpts = o["response"]["ts"].ToString();
                    //--   --
                }
            }
            catch (Exception e) {

                SendMessage(new Message(_my_id, e.Message + $" at {DateTime.Now}"));
                goto establis_LP;
            }
            CallLP(); // Sendint next request ( first was just for setup )
        }



        public async void HandleUnreadDialogs() {

                string rq = RequestBuilder.GetUnreadDialogs();
                HttpResponseMessage answer = await client.GetAsync(rq);
                string answer_message = await answer.Content.ReadAsStringAsync();
                DM(answer_message);
                
            

        }

        private void DM(string json_string) // Deserialize message
        {
            JObject server_answer = JObject.Parse(json_string);
            foreach (var item in server_answer["response"]["items"]) {
                string from_user = item["message"]["user_id"].ToString();
                string text = item["message"]["body"].ToString();

                Message unanswered = new Message(from_user, text);
                _tower.ChimeIncomingMessage(unanswered);
            }


        }





        private static string _my_id = "80314023";


    }
}
