using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.Edna.BiliBili.Output
{
    public class ResultMsg<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; } = int.MinValue;
        [JsonProperty("sessage")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
