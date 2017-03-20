﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace vk_dotnet.Methods
{
    public class Messages_Methods : Method
    {


        

        public Messages_Methods(string token) : base(token)
        {  }
        public async Task Send(string user_id, string message)
        {
            string request = ApiMethods.GetMethodUri("messages.send",
                $"user_id={user_id}",
                $"message={message}",
                $"access_token={_token}");
            string response = await SendGetAsync(request);

            Console.WriteLine(response);
        }

    }
}
