using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vk_dotnet.Objects
{
    public class GetLongPollResponse
    {
        public string key { get; set; }
        public string server { get; set; }
        public string ts { get; set; }
        public string pts { get; set; }
    }
}
