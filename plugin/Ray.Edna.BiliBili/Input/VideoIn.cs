using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.Edna.BiliBili.Input
{
    public class VideoIn
    {
        public long Vmid { get; set; }

        public int Pn { get; set; } = 1;

        public int Ps { get; set; } = 20;

        public string Order { get; set; } = "desc";

        public string Order_type { get; set; } = "attention";

        public string Jsonp { get; set; } = "jsonp";

    }

    public class VideoSearch
    {
        public int Tid { get; set; } = 0;
        public string Keyword { get; set; } = "";
        public string Order { get; set; } = "pubdate";
        public string Jsonp { get; set; } = "jsonp";
        public long Mid { get; set; }
        public int Pn { get; set; } = 1;
        public int Ps { get; set; } = 20;
    }
}
