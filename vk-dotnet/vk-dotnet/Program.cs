using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using vk_dotnet.Objects;
using System.Collections.Generic;

namespace vk_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Session s = new Session();

            Console.WriteLine(ApiMethods.GetMethodUri("users.get"));

            AsyncMothefucker(s);

            Console.ReadKey();
        }

        static async Task AsyncMothefucker(Session s)
        {
            List<User> result = await s.Users.Get("94394992", "1" , "123456");

            result.ForEach(user => Console.WriteLine(user.first_name));
            

        } 


    }
}