using System;
using System.Threading;

namespace vk_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Session s = new Session();
            s.users.get("210700286");
            Thread.Sleep(9999);
        }
    }
}