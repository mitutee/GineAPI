using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vk_dotnet.Local;
using vk_dotnet.Methods;
using vk_dotnet.Objects;

namespace vk_dotnet
{
    /// <summary>
    /// Wrapper for vk API;
    /// Used for automating tasks;
    /// </summary>
    public class BotClient
    {
        #region Static Members
        public static bool TokenIsValid(string token)
        {
            try {
                int permissions = Account_Methods.GetAppPermissions(token).Result;
                return _containsMessageFlag(permissions);

            }
            catch (Exception e) {

                throw e;
            }
        }

        private static bool _containsMessageFlag(int permissions)
        {
            return (permissions & 4096) != 0;
        }

        private static LocalConfig _configurator = LocalConfig.TheInstance;

        public static bool TryGetLogin(out string login, out string password)
        {
            return _configurator.TryGetLoginPass(out login, out password);
        }

        #endregion

        /// <summary>
        /// Message from users in this set are ignored;
        /// ( By default host id is added to prevent snowball answering ).
        /// </summary>
        public HashSet<string> BlackList = new HashSet<string>();

        public readonly Session _s;

        #region Public Fields
        public string _my_id { get; private set; }

        #endregion

        #region Constructors
        //public BotClient(string login, string password)
        //{
        //    _s = new Session(login, password);
        //    _s.SignInAsync();//.Wait();
        //    // _getMyId();
        //}
        public BotClient(string token)
        {
            _s = new Session(token);
            _s.SignInAsync();// Wait();

            BlackList.Add(_getMyId().Result);



        }

        #endregion

        #region Public Methods
        public void StartListeningAsync()
        {
            _s.LongPollServer.GetLongPollServer().Wait();
            Task.Factory.StartNew(_startListeningAsync);

        }

        public void StartListening()
        {
            _s.LongPollServer.GetLongPollServer().Wait();
            _startListening();
        }

        public async Task<string> SendTextMessageAsync(string user_id, string text)
        {
            return await _s.Messages.Send(user_id, text);
        }


        #endregion

        #region Events
        public delegate void BotEventHandler<TArgs>(BotClient sender, TArgs e);

        public BotEventHandler<Message> IncommingTextMessage;

        private void OnIncomingTextMessage(Message arg)
        {
            IncommingTextMessage?.Invoke(this, arg);
        }

        #endregion

        #region Private Methods
        private async Task _startListeningAsync()
        {
            while (true) {
                List<List<string>> updates = await _s.LongPollServer.CallLongPoll();
                List<Message> incoming_messages = LongPoll_Methods.ParseEventForMessages(updates);
                foreach (var msg in incoming_messages) {
                    if (BlackList.Contains(msg.User_id)) {
                        Console.WriteLine(
                            $"Ignoring message from {msg.User_id} cause it is in a black list.");
                        continue;
                    };
                    Message args = msg;
                    OnIncomingTextMessage(args);

                }
            }
        }

        private void _startListening()
        {
            while (true) {
                List<List<string>> updates = _s.LongPollServer.CallLongPoll().Result;
                //Console.WriteLine("Updates: ");
                //updates.ForEach(li => {
                //    Console.WriteLine("----------");
                //    li.ForEach(str => Console.WriteLine(str));
                //    Console.WriteLine("----------");
                //    });
                //Console.WriteLine("****************************");
                List<Message> incoming_messages = LongPoll_Methods.ParseEventForMessages(updates);
                foreach (var msg in incoming_messages) {
                    if (BlackList.Contains(msg.User_id)) {
                        Console.WriteLine(
                            $"Ignoring message from {msg.User_id} cause it is in a black list.");
                        continue;
                    };
                    Message args = msg;
                    OnIncomingTextMessage(args);

                }
            }
        }

        private async Task<string> _getMyId()
        {
            try {
                List<User> answer = await _s.Users.Get();
                User me = answer[0];
                Console.WriteLine("My id is: " + me.id);
                return me.id;
            }
            catch (AutorizationException e) when (e.Message.Contains("method is unavailable with group")) {
                Console.WriteLine("Group token is given.");
                return "";
            }
            catch (AutorizationException e) {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
        #endregion

    }
}

/*

#region Static Members

#endregion

#region Private Fields

#endregion

#region Public Fields

#endregion

#region Constructors

#endregion

#region Public Methods

#endregion

#region Events

#endregion

#region Private Methods 

#endregion

*/
