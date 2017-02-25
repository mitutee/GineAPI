using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VkNetGine
{
    public static class JsonMituteeDeserializator
    {
        public static VkEvent ConvertToEvent(string json)
        {
            VkEvent e = new VkEvent();
            string ts = "";
            List<List<string>> up = new List<List<string>>();
            bool isTsSettuped = false;
            bool isUpdatesSettuped = false;


            try
            {
                #region Loop through string
                for (int i = 0; i < json.Length; i++)
                {
                    #region find ts value
                    if (!isTsSettuped && json[i] == 't' && json[++i] == 's')
                    {
                        while (json[i] != ':')
                            i++;
                        i++;
                        for (; json[i] != ','; i++)
                        {
                            ts += json[i];
                        }
                        e.ts = ts;
                        isTsSettuped = true;

                    }
                    #endregion
                    if (isTsSettuped && !isUpdatesSettuped && json.Substring(i).Contains("updates"))
                    {

                        while (json[i++] != '[') ;
                        // 
                        getsubarr:
                        while (json[i++] != '[') ; // start point
                        i--;
                        string subarr = String.Empty;
                        while (json[i++] != ']') // find end
                            subarr += json[i];
                        List<string> singleUpdate = getJSarray(subarr);
                        if (singleUpdate.Count != 0)
                        {
                            if (singleUpdate[0] == "4" && (Convert.ToInt32(singleUpdate[2]) & 2) == 0)
                            {
                                e.IncommingMessages.Add(new Message(singleUpdate[3], singleUpdate[6]));
                            }
                        }
                        up.Add(singleUpdate);
                        if (json[++i] == ',') goto getsubarr;
                        isUpdatesSettuped = true;

                        e.Updates = up;
                        //
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            return e;
        }


        private static List<string> getJSarray(string jsarr)
        {
            List<string> l = new List<string>();
            for (int i = 0; i < jsarr.Length; i++)
            {
                string el = string.Empty;
                while (i < jsarr.Length && jsarr[i] != ',')
                {
                   // if (jsarr[i] == '\\' || jsarr[i] == '"') continue;
                    el += jsarr[i];
                    i++;
                }
                l.Add(el);
            }
            return l;
        }


    }
}
