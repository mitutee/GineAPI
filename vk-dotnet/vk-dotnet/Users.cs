using System;
using System.Net.Http;

namespace vk_dotnet
{
    internal class Users
    {
        private static Users theInstance;

        internal static Users TheInstance {
            get {
                if (theInstance == null)
                {
                    theInstance = new Users();
                }
                return theInstance;
            }
        }

        internal async void  get(string user_id)
        {
            HttpClient c = new HttpClient();

            var r = await c.GetAsync(ApiMethods.GetMethodUri("users.get", $"user_id={user_id}"));

            Console.WriteLine(await r.Content.ReadAsStringAsync());
        }
    }
}