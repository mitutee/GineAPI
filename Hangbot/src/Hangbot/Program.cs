using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VkNetGine;

namespace Hangbot
{
    public class Program {


        public static void Main() {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            BotInit();
        }
        public static void BotInit() {
            string to = "80314023";
            string token = "cb8a8c455efa18e4d57cde56e25bb66dc56a33d56eae17d9ddade834c88fe21c5e052b114d1ab89869fbe";
            Hangbot John = new Hangbot(token);
            // John.SendCustomMessage("80314023", $"Awake on {DateTime.Now}");
            //Task.Factory.StartNew(Asynchronously);
            while (true) {
                John.SendCustomMessage(_assistant_id, $"Awake on {DateTime.Now}");
                John.ReCheck();
                Thread.Sleep(30000);
            }



        }

        public static string _assistant_id = "414460724";



    }




}
