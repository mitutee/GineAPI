using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace vk_dotnet.Objects
{
    public class Message
    {
        private string peer_id;
        public int Id { get; set; }

        public string Peer_id {
            get { return peer_id; }
            set {
                peer_id = value;

                long p_id = Int64.Parse(value);
                if (p_id > 0 && p_id < 2e9) {
                    PeerType = ChatType.User;
                }
                else if (p_id < 0 && p_id > -2e9) {
                    PeerType = ChatType.Group;
                }
                else if (p_id > 2e9) {
                    PeerType = ChatType.Chat;
                }
                else if (p_id < -2e9) {
                    PeerType = ChatType.Email;
                }
            }
        }

        public string From_id { get; set; }
        public int Date { get; set; }
        public int Read_state { get; set; }
        public int Out { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public List<Message> Fwd_messages { get; set; }
        public int Emoji { get; set; }
        public int Important { get; set; }
        public int Deleted { get; set; }
        public string Random_id { get; set; }

        public ChatType PeerType;

        public enum ChatType { User, Group, Chat, Email }
    }
}
