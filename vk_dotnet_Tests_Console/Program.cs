using System;
using vk_dotnet;
using vk_dotnet.Objects;

namespace vk_dotnet_Tests_Console
{
    class Program
    {
        private static string myToken = "98ff2ea0959699310da1594655ec3465dfba3f2a27ea61ef4139016fb5dd6ac1a9335a61616e38eb1ad6a";

        static void Main(string[] args)
        {
            BotClient MyBot = new BotClient(myToken);
            MyBot.IncomingTextMessage += IncomingMessageHandler;
            MyBot.StartListeningAsync();

            Console.ReadKey(); // Otherwise program will finish immediately
        }

        private static void IncomingMessageHandler(BotClient sender, Message e)
        {
            sender.SendTextMessageAsync(e.User_id.ToString(), "Hello from MyBot!");
        }
    }
}