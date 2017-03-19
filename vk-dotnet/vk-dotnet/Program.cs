using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using vk_dotnet.Objects;
using vk_dotnet.Local;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net;
using Newtonsoft.Json;
using vk_dotnet.Methods;
using Newtonsoft.Json.Linq;

namespace vk_dotnet
{


    class Program
    {
        public static string t = "b22bbc07a3602aa6dae05ab3fefa7ad0b72a9349ad4cd6bcc0e268772ec83d2e0fdac1a9f6325f83c7100";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            LocalConfig.TheInstance.TryGetLoginPass(out string l, out string p);

            Console.WriteLine(l);

            // Async();
            Console.ReadKey();
        }

        public static async void Async()
        {
            Session s = new Session(t);
            await s.SignIn();
            await s.LongPollServer.GetLongPollServer();
            while (true)
            {
                await s.LongPollServer.CallLongPoll();
                Thread.Sleep(5000);
            }

        }


    }
}