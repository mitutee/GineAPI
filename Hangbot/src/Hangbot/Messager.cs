using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNetGine;

namespace Hangbot
{
    public class Messager
    {
        private API _api;

        public API Api {
            get {
                return _api;
            }

            set {
                _api = value;
            }
        }

        public Messager(API a) {
            Api = a;
        }


        public void SendCustomMessage(string to, string what) {
            Api.SendMessage(new Message(to, what));
        }




    }
}
