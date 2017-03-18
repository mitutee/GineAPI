using System;
using System.Net.Http;
using System.Threading.Tasks;
using vk_dotnet.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace vk_dotnet.Methods
{
    public class Users_Methods : Method
    {
        private string _tocken;

        public Users_Methods() { }

        public Users_Methods(string tocken)
        {
            _tocken = tocken;
        }

        public async Task<List<User>> Get(params string[] user_ids)
        {
            string request = ApiMethods.GetMethodUri("users.get",
                $"user_ids={String.Join(",", user_ids)}");

            string response = await SendRequestAsync(request);

            Console.WriteLine(response);

            JToken json_users_array = JObject.Parse(response)["response"];

            List<User> list_of_users = new List<User>();

            foreach(var user in json_users_array)
            {
                Console.WriteLine(user.ToString());
                list_of_users.Add(JsonConvert.DeserializeObject<User>(user.ToString()));
            }

 
            return list_of_users;
        }
    }
}