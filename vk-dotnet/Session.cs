using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using vk_dotnet.Methods;
using System.Threading.Tasks;

namespace vk_dotnet
{
    public class Session
    {
        private string _token { get; set; }
        private string _login { get; set; }
        private string _pass  { get; set; }


//        public Session(){}
        public Session(string token)
        {
            _token = token;
        }

        public Session(string login, string password)
        {
            _login = login;
            _pass = password;
        }

        public async Task SignInAsync()
        {
            if(_token == null)
            {
                _token = await Method.LoginPasswordAutorization(_login, _pass);
            }
            init(_token);
        }
        private void init(string token)
        {
            Users = new Users_Methods(token);
            Messages = new Messages_Methods(token);
            LongPollServer = new LongPoll_Methods(token);
        }

        public Users_Methods Users;

        public Messages_Methods Messages;

        public LongPoll_Methods LongPollServer;
    }
}
