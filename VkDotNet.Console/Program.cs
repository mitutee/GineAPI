using System;
using static System.Console;
using VkDotNet.Core.QueryBuilder;
using VkDotNet;
namespace VkDotNet.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var query = 
                WallQueryBuilder
                .Wall
                .Get()
                .Token("5ca5af5fa9027b475508400f28be5563d6e10d775c6db7fe0bc8adf0c5c05374034e4e17961e3ab7a6c5b")
                .OwnerId("1");
            var result = SideEffects.Execute(query.Query);
            WriteLine(result);
        }
    }
}
