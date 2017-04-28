using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using vk_dotnet.Objects;
using Newtonsoft.Json;

namespace vk_dotnet.Methods
{
    public class Account_Methods : Method
    {
        public Account_Methods(string token) : base(token)
        {
        }
        #region API Methods

        //public async Task<User> GetInfo()
        //{
        //    string request = GetMethodUri("account.getInfo",
        //        $"access_token={_token}");
        //    string response = await CallApiAsync(request);
        //    User me = JsonConvert.DeserializeObject<User>(response);
        //    return me;
        //}

        
        public async Task<User> GetProfileInfo()
        {

            string request = GetMethodUri("account.getInfo",
                $"access_token={_token}");
            string response = await SendGetAsync(request);
            Console.WriteLine(response);
            User me = new User();
            return me;
        }

        /// <summary>
        /// Получает настройки текущего пользователя в данном приложении.
        /// </summary>
        /// <returns>После успешного выполнения возвращает битовую маску настроек текущего пользователя в данном приложении.</returns>
        public static async Task<int> GetAppPermissions(string token)
        {
            string request = GetMethodUri("account.getAppPermissions",
                $"access_token={token}");
            string response = await CallApiAsync(request);
            return Int32.Parse(response);
        } 
        #endregion
    }
}
