using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.Edna.BiliBili.Output
{
    public class VideoFollowingOut
    {
        [JsonProperty("list")]
        public List<UpInfo> List { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
    public class UpInfo
    {
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("uname")]
        public string Name { get; set; }
    }
}
