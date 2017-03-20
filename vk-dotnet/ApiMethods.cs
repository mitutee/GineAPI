using System;
using System.Collections.Generic;
using System.Text;

namespace vk_dotnet
{
    class ApiMethods
    {

        private static string _mainURI = "https://api.vk.com/method";


        public static string GetMethodUri(string method, params string[] parameters){

            string prms = "";
            for (int i = 0; i < parameters.Length; i++)
            {
                prms +=   parameters[i] + '&';
            }

            string request_uri = $"{_mainURI}/{method}?{prms}v=5.62";
            return request_uri;


        }

        public static string InitLP(string token) =>
            $"{_mainURI}/messages.getLongPollServer?use_ssl=1&need_pts=1?v=5.41&access_token={token}&count=10&offset=0";
        //--  --
        public string SendMsg(string id, string msg, string token) =>
               $"{_mainURI}/messages.send?user_id={id}&message={msg}&v=5.41&access_token={token}&count=10&offset=0";

        //--  --
        public static string CallLP(string lpserver, string lpkey, string lpts) =>
            $"https://{lpserver}?act=a_check&key={lpkey}&ts={lpts}&wait=25&mode=2&version=1";

        //--  --
        public static string GetUnreadDialogs(string token) =>
            $"{_mainURI}/messages.getDialogs?&unanswered=1&access_token={token}&v=5.62";

    }
}
