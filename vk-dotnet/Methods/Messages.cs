using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace vk_dotnet.Methods
{
    public class Messages_Methods : Method
    {
      
        public Messages_Methods(string token) : base(token)
        {  }
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
        #endregion
    }
}
