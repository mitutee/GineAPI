using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vk_dotnet.Objects
{
    public class CallLongPollResponse
    {
        public string ts { get; set; }
        public List< List<string> > Updates { get; set; }
        public int? Failed { get; set; }
    }
}
