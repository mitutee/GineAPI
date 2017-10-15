using System;
using static System.Console;
using VkDotNet.Core.QueryBuilder;
using VkDotNet.Core;
namespace VkDotNet.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new WallQueryBuilder(I.BlankQuery).WithAccessToken("fd").Build();
            WriteLine(q);
        }
    }
}
