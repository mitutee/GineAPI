using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkNetGine
{
    public class Message
    {
        public string Target { get; set; }
        public string Text { get; set; }

        public Message() { }
        public Message(string t, string text)
        {
            Target = t;
            Text = text;
        }
    }
}
