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
        public static string t = "b22bbc07a3602aa6dae05ab3fefa7ad0b72a9349ad4cd6bcc0e268772ec83d2e0fdac1a9f6325f83c7100";
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Session s = new Session(t);

            while (true)
            {
                string m = Console.ReadLine();
                s.Messages.Send("80314023", m);
            }
            

            Console.ReadKey();
        }

        static async Task AsyncMothefucker(Session s)
        {
            List<User> result = await s.Users.Get("94394992", "1" , "123456");

            result.ForEach(user => Console.WriteLine(user.first_name));
            

        } 


    }
}