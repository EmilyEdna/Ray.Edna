using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.Edna.BiliBili.Output
{
    public class VideoFollowingOut
    {
        public List<UpInfo> List { get; set; }

        public int Total { get; set; }
    }
    public class UpInfo
    {
        public long Mid { get; set; }

        public string Uname { get; set; }
    }
}
