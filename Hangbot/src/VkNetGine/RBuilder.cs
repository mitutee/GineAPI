using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkNetGine
{
    public static class RequestBuilder
    {

        #region FIELDS
        //------------------------------------ FIELDS ----------------------------------------
        private static string _token;
        private static string _mainURI = "https://api.vk.com/method/";

        private static object _lpserver;
        private static object _lpkey;
        private static object _lpts;





        #region Properties
        public static string Token {
            get {
                return _token;
            }

            set {
                _token = value;
            }
        }

        public static object Lpserver {
            get {
                return _lpserver;
            }

            set {
                _lpserver = value;
            }
        }

        public static object Lpkey {
            get {
                return _lpkey;
            }

            set {
                _lpkey = value;
            }
        }

        public static object Lpts {
            get {
                return _lpts;
            }

            set {
                _lpts = value;
            }
        }

        #endregion
        //------------------------------------------------------------------------------------

        #endregion
        //--  --
        public static string InitLP() =>
            $"{_mainURI}messages.getLongPollServer?use_ssl=1&need_pts=1?v=5.41&access_token={_token}&count=10&offset=0";
        //--  --
        public static string SendMsg(string id, string msg) =>
            $"{_mainURI}messages.send?user_id={id}&message={msg}&v=5.41&access_token={_token}&count=10&offset=0";

        //--  --
        public static string CallLP() => 
            $"https://{_lpserver}?act=a_check&key={_lpkey}&ts={_lpts}&wait=25&mode=2&version=1";

        //--  --
        public static string GetUnreadDialogs() => 
            $"https://api.vk.com/method/messages.getDialogs?&unanswered=1&access_token={_token}&v=5.62";

    }
}
