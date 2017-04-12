using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vk_dotnet;
using vk_dotnet.Objects;

namespace vk_dotnet_Console
{
    class Program
    {
        private static string token = "e046815926afa65c10540afa418fa3fb7d3a1d3c92e8faa8f12682a4cd7da4ad90cadb1db693b15e2ead4";

        static void Main(string[] args)
        {
            BotClient cl = new BotClient(token);
            cl.IncomingMessage += MessageHandler;
            var a = cl._s.Users.Get().Result;
            cl.StartListeningAsync();

            Console.ReadKey();
        }

        private static void MessageHandler(BotClient sender, Message e)
        {
            sender.SendTextMessageAsync(e.User_id.ToString(), "Hello world");
        }
    }
}
