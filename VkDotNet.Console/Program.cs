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
            while (true)
            {
                string token = "b95e0d29d252f1833b5d1c5fa32cccf19997d3d377472434c87fe5cfee257172004e2dfd42f0705074955";
                string messageToSend = $"{DateTime.Now}";//- from {System.Environment.MachineName}";
                //messageToSend += $"\n In folder {System.Environment.CurrentDirectory}";
                MessagesSendQuery messagesSendQuery = Vk
                    .Messages
                    .Send()
                    .WithAccessToken(token)
                    .WithUserId("215123436")
                    .WithMessage(messageToSend);
                string res = messagesSendQuery
                    .Execute();
                WriteLine(res);
                System.Threading.Thread.Sleep(2500);
            }
        }
    }
}
