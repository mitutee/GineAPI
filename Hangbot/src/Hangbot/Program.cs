using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VkNetGine;

namespace Hangbot
{
    public class Program
    {
        public static void Main()
        {
            string to = "80314023";




            

        }
        public static void BotInit()
        {
            string token = "cb8a8c455efa18e4d57cde56e25bb66dc56a33d56eae17d9ddade834c88fe21c5e052b114d1ab89869fbe";
            Hangbot John = new Hangbot(token);
            John.SendCustomMessage("80314023", $"Awake on {DateTime.Now}");

            while (true)
            {
                John.SendCustomMessage("80314023", $"I am alive, my Master! Server time is: {DateTime.Now}");
                Thread.Sleep(60000);
            }
        }



    }




    public class Hangbot
    {
        private API api;
        private ClockTower _tower; //Subsribing for events there


        public void SendCustomMessage(string to, string what)
        {
            api.SendMessage(new Message(to, what));
        }

        public Hangbot(string token)
        {
            _tower = new ClockTower();
            api = new API(token, _tower);

            _tower.Chime += (msg) => {
                HandleIncomingMessage(msg);
                Console.WriteLine("NEW MESSAGE! HANDLING...");
            };
        }

        private void HandleIncomingMessage(Message msg)
        {
            string answer = $"Вы пишете мне : {msg.Text}\n"
                +"Но сейчас я не смогу ответить красноречиво и ничем Вас удивить((😰\nИдет интенсивная работа над моим совершенствованием!";
            api.SendMessage(new Message(msg.Target, answer));
        }
    }
}
