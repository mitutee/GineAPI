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
            var lp = Vk
                .Messages
                .GetLongPollServer()
                .WithAccessToken(token)
                .Execute();
            while (true)
            {
                string token = "b95e0d29d252f1833b5d1c5fa32cccf19997d3d377472434c87fe5cfee257172004e2dfd42f0705074955";
                var r = Vk.LongPoll
                    .WithKey(lp.Key)
                    .WithServer(lp.Server)
                    .WithTs(lp.Ts)
                    .Execute();


                WriteLine(r);
                System.Threading.Thread.Sleep(2500);
            }
        }
    }
}
