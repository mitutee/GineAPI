using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;


namespace vk_dotnet
{
    class Session
    {
        private HttpClient client = new HttpClient();

        public Session(){}


        public Users users = Users.TheInstance;
    }
}
