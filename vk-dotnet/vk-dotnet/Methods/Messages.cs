using System;
using System.Collections.Generic;
using System.Text;

namespace vk_dotnet.Methods
{
    public class Messages_Methods : Method
    {
        private string _token;

        

        public Messages_Methods(string token) : base(token)
        {  }
        public async void Send(string user_id, string message)
        {
            string request = ApiMethods.GetMethodUri("messages.send",
                $"user_id={user_id}",
                $"message={message}");
            string response = await SendGetAsync(request);

            Console.WriteLine(response);
        }

    }
}
