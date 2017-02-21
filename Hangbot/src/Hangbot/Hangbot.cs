using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNetGine;

namespace Hangbot
{
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
                + "Но сейчас я не смогу ответить красноречиво и ничем Вас удивить((😰\nИдет интенсивная работа над моим совершенствованием!";
            api.SendMessage(new Message(msg.Target, answer));
        }
    }
}
