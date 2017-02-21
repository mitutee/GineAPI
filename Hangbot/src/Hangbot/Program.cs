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




}
