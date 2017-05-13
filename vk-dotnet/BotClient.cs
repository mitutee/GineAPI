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
    public class BotClient : Session
    {
        #region Static Members

        /// <summary>
        /// Checks if user token allows send messages;
        /// </summary>
        /// <param name="token">!USER! token to check</param>
        /// <returns></returns>
        public static bool UserToken_MessagesAllowed(string token)
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
        public BotClient(string token) : base(token)
        {            
            SignInAsync();// Wait();
            //     BlackList.Add(_getMyId().Result);
        }

        #endregion

        #region Public Methods
        public void StartListeningAsync()
        {
            LongPollServer.GetLongPollServer().Wait();
            Task.Factory.StartNew(_startListeningAsync);

        }

        public void StartListening()
        {
            LongPollServer.GetLongPollServer().Wait();
            _startListening();
        }

        public async Task<string> SendTextMessageAsync(string user_id, string text)
        {
            return await Messages.Send(user_id, text);
        }


        #endregion

        #region Events
        public delegate void BotEventHandler<TArgs>(BotClient sender, TArgs e);

        public BotEventHandler<Message> IncomingTextMessage;

        public void OnIncomingTextMessage(Message arg)
        {
            IncomingTextMessage?.Invoke(this, arg);
        }

        #endregion

        #region Private Methods
        private async Task _startListeningAsync()
        {
            while (true) {
                List<List<string>> updates = await LongPollServer.CallLongPoll();
                List<Message> incoming_messages = LongPoll_Methods.ParseEventForMessages(updates);
                foreach (var msg in incoming_messages) {
                    if (BlackList.Contains(msg.Peer_id)) {
                        Console.WriteLine(
                            $"Ignoring message from {msg.Peer_id} cause it is in a black list.");
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
                List<List<string>> updates = LongPollServer.CallLongPoll().Result;
                //Console.WriteLine("Updates: ");
                //updates.ForEach(li => {
                //    Console.WriteLine("----------");
                //    li.ForEach(str => Console.WriteLine(str));
                //    Console.WriteLine("----------");
                //    });
                //Console.WriteLine("****************************");
                List<Message> incoming_messages = LongPoll_Methods.ParseEventForMessages(updates);
                foreach (var msg in incoming_messages) {
                    if (BlackList.Contains(msg.Peer_id)) {
                        Console.WriteLine(
                            $"Ignoring message from {msg.Peer_id} cause it is in a black list.");
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
                List<User> answer = await Users.Get();
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
