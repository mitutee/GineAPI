using System;
using vk_dotnet;
using vk_dotnet.Objects;

namespace vk_dotnet_Tests_Console
{
    class Program
    {
        private static string myToken = "a7474da0059a51709b95871b85907291756c23f98555459def4256f03a58a376ced01d2b8373ae738e7bc";

        static void Main(string[] args)
        {
            BotClient MyBot = new BotClient(myToken);

            Console.WriteLine(MyBot.Groups.GetCallbackConfirmationCode("146335820"));

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