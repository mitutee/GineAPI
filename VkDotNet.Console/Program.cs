using System;
using static System.Console;
using VkDotNet.Core;
using VkDotNet.Core.QueryBuilders;
using Microsoft.FSharp.Core;
using System.IO;

namespace VkDotNet.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            StartListening();
        }

        static void StartListening()
        {

            var bot = new LongPollListener(token);
            bot.StartListening();

        }
    }
}
