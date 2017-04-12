## Использование

```C#
    class Program
    {
        private static string myToken = "<my_token>";

        static void Main(string[] args)
        {
            BotClient MyBot = new BotClient(myToken);
            MyBot.IncomingMessage += IncomingMessageHandler;
            MyBot.StartListeningAsync();

            Console.ReadKey(); // Otherwise program will finish immediately
        }

        private static void IncomingMessageHandler(BotClient sender, Message e)
        {
            sender.SendTextMessageAsync(e.User_id.ToString(), "Hello from MyBot!");
        }
    }
