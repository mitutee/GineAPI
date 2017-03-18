using System;
using System.Collections.Generic;
using System.Text;

namespace vk_dotnet.Methods
{
    public class Messages_Methods : Method
    {
        private string _token;

        public Messages_Methods(){}

        public Messages_Methods(string token)
        {
            _token = token;
        }
        public async void Send(string user_id, string message)
        {
            string request = ApiMethods.GetMethodUri("messages.send",
                $"user_id={user_id}",
                $"message={message}",
                $"access_token={_token}");
            string response = await SendRequestAsync(request);

            Console.WriteLine(response);
        }

    }
}
