using System;
using vk_dotnet;
using vk_dotnet.Objects;

namespace vk_dotnet_Tests_Console
{
    class Program
    {
        private static string myToken = "77735e98916aff3c0773497c6023269cec0806362c1befdeef34d6ef1603c70537eb54614fde5aa088155";

        static void Main(string[] args)
        {
            BotClient MyBot = new BotClient(myToken);
            MyBot.IncomingTextMessage += IncomingMessageHandler;
            MyBot.StartListeningAsync();

            Console.ReadKey(); // Otherwise program will finish immediately
        }

        private static void IncomingMessageHandler(BotClient sender, Message e)
        {
            if (e.PeerType == Message.ChatType.Group) {
                Console.WriteLine("Group!");
            }
            sender.SendTextMessageAsync(e.Peer_id.ToString(), "Hello from MyBot!").Wait();
        }
    }
}