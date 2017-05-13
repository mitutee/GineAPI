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
            var res =  CallApiAsync(request).Result;
            return JsonConvert.DeserializeObject<GroupConfirmCode>(res).Code;
        }

    }
}
