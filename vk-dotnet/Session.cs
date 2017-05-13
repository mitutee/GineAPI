using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using vk_dotnet.Methods;
using System.Threading.Tasks;
using vk_dotnet.Objects;

namespace vk_dotnet
{
    public class Session
    {
        private string _token { get; set; }

        #region API METHODS
        public Users_Methods Users;

        public Messages_Methods Messages;

        public LongPoll_Methods LongPollServer;

        public Account_Methods Account;

        public Groups_Methods Groups;


        /// <summary>
        /// Initializing Methods with the given token
        /// </summary>
        /// <param name="token">Token to be passed into methods</param>
        private void init(string token)
        {
            Users = new Users_Methods(token);
            Messages = new Messages_Methods(token);
            LongPollServer = new LongPoll_Methods(token);
            Account = new Account_Methods(token);
        }
        #endregion


        public Session(string token)
        {
            _token = token;
            SignInAsync();
        }



        public void SignInAsync()
        {
            //User me = Account.
            init(_token);
        }

    }
}
