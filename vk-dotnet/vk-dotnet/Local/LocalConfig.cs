using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace vk_dotnet.Local
{
    public class LocalConfig
    {
        private readonly string tokens_file = "loc_toc.json";



        private static LocalConfig _theInstance;
        public static LocalConfig TheInstance {

            get {
                if (_theInstance == null)
                    _theInstance = new LocalConfig();
                return _theInstance;
            }
        }
        private LocalConfig() { }
        private Dictionary<string, string> tokens;


        internal bool TryGetToken(string login, out string token)
        {
            loadTokens();
            if (tokens.TryGetValue(login, out token))
                return true;
            return false;
        }

        internal void CasheToken(string login, string token)
        {
            loadTokens();
            tokens.Remove(login);
            tokens.Add(login, token);
            string json = JsonConvert.SerializeObject(tokens);
            System.IO.File.WriteAllText(tokens_file, json);
        }

        /// <summary>
        /// Loading tokens from specified config file and instantiate proper dictionary ( tokens )
        /// If file
        /// </summary>
        private void loadTokens()
        {
            string json;
            try
            {
                json = System.IO.File.ReadAllText(tokens_file);
                if (json == String.Empty)
                    throw new ArgumentNullException("Config file is blank.");
            }
            //Something is wrong, instantiate <tokens> as an empty Dict
            catch (Exception e)
            {
                tokens = new Dictionary<string, string>();
                return;
            }
            tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
