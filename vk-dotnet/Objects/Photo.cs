using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vk_dotnet.Objects
{


public class Photo
{
    public int id { get; set; }
    public int album_id { get; set; }
    public int owner_id { get; set; }
    public List<Size> sizes { get; set; }
    public string text { get; set; }
    public int date { get; set; }

    public class Size
    {
        public string src { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string type { get; set; }
    }
}
}
