using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vk_dotnet.Objects;

namespace vk_dotnet.Methods
{
    public class Groups_Methods : Method
    {
        public Groups_Methods(string token) : base(token)
        { }
        public string GetCallbackConfirmationCode(string group_id)
        {
            string request = GetMethodUri("groups.getCallbackConfirmationCode",
                        $"group_id={group_id}",
                        $"access_token={_token}");
            var res = CallApiAsync(request).Result;
            return JsonConvert.DeserializeObject<GroupConfirmCode>(res).Code;
        }
        public string SetCallbackServer(string group_id, string URI)
        {
            string request = GetMethodUri("groups.setCallbackServer",
                $"group_id={group_id}",
                $"server_url={URI}",
                $"access_token={_token}");
            return SendGetAsync(request).Result;
        }

        public string SetCallbackSettings(string group_id, bool message_new = true)
        {
            string request = GetMethodUri("groups.setCallbackSettings",
    $"group_id={group_id}",
    $"message_new={1}",
    $"access_token={_token}");
            return SendGetAsync(request).Result;
        }
    }
}
