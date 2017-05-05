using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vk_dotnet.Objects;

namespace vk_dotnet.Methods
{
    public class Messages_Methods : Method
    {

        public Messages_Methods(string token) : base(token)
        { }
        #region API Methods
        /// <summary>
        /// Отправляет сообщение.
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<string> Send(string user_id, string message)
        {
            string request = GetMethodUri("messages.send",
                $"user_id={user_id}",
                $"message={message}",
                $"access_token={_token}");
            string response = await CallApiAsync(request);
            return response;
        }

        public async Task<List<Message>> GetHistory(string user_id, byte count, int offset)
        {
            string request = GetMethodUri("messages.getHistory",
                   $"user_id={user_id}",
                   $"count={count}",
                   $"offset={offset}",
                   $"access_token={_token}");
            string response = await CallApiAsync(request);
            JObject o = JObject.Parse(response);
            return JsonConvert.DeserializeObject<List<Message>>(o["items"].ToString());
        }
        #endregion
    }
}
