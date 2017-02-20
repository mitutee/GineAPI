using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkNetGine
{
    public class VkEvent
    {
        public string ts;
        public List<List<string>> Updates;
        public List<Message> IncommingMessages;

        public bool HasIncommingMsg() => IncommingMessages.Count > 0;

        public VkEvent()
        {
            IncommingMessages = new List<Message>();
        }
    }
}
