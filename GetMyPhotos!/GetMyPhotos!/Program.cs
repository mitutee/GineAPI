using System;
using vk_dotnet;

namespace GetMyPhotos_
{
    class Program
    {
        static string token = "23786e7e58fdaa2596eb15d317d5495f5b726731d4bc08b0c03877e7e840eb23540126d27f9bd2dc5537c";

        static BotClient _bot = new BotClient(token);
        static void Main(string[] args)
        {
            _bot.Photos.GetAll();
        }
    }
}