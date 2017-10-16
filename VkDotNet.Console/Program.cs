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
            string token = "";
            MessagesSendQuery messagesSendQuery = Vk
                .Messages
                .Send()
                .WithAccessToken(token)
                .WithUserId("215123436")
                .WithMessage(token);
            var res = messagesSendQuery
                .Execute();
            WriteLine(res);
        }
    }
}
