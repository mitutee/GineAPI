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




            Session s = new Session("", "");

            s.SignIn();




            //Auth("+380505102671", "oJolgolo243VK");

            Console.ReadKey();
        }
        //\"lg_h\" value=\"([a - z0 - 9] +)\"



    }
}