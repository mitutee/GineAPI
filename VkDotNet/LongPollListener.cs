using VkDotNet.Core.QueryBuilders;
using VkDotNet.Core;
using System;

namespace VkDotNet
{
    public class LongPollListener : IVkBot
    {
        public event VkBotEventHandler<Message> MessageReceived;
        private void OnMessageReceived(Message m)
        {
            MessageReceived?.Invoke(this, m);
        }

        private string _server;
        private string _key;
        private string _ts;
        private bool _weAreListening = true;

        public string AccessToken { get; }

        public LongPollListener(string accessToken)
        {
            AccessToken = accessToken;
        }

        public void StartListening()
        {
            InitLongPoll();

            while (_weAreListening)
            {

                var response = CallLongPoll();


                foreach (var m in response.MessageUpdates) OnMessageReceived(m);

                _ts = response.Ts;
            }
        }



        private void InitLongPoll()
        {
            var longPollInfo = Vk.Messages
                .GetLongPollServer()
                .WithAccessToken(AccessToken)
                .Execute();

            _server = longPollInfo.Server;
            _key = longPollInfo.Key;
            _ts = longPollInfo.Ts;
        }

        private LongPollResponse CallLongPoll() =>
            Vk.LongPoll.WithServer(_server).WithKey(_key).WithTs(_ts).Execute();
    }
}