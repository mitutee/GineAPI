using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using vk_dotnet.Methods;

namespace vk_dotnet
{
    class Session
    {
        

        public Session(){}
        public Session(string token)
        {
            Users = new Users_Methods(token);
            Messages = new Messages_Methods(token);

        }

        public Users_Methods Users;

        public Messages_Methods Messages;
    }
}
