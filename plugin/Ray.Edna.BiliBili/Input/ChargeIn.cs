using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.Edna.BiliBili.Input
{
    public class ChargeIn
    {
        public decimal bp_num { get; set; }
        public long up_mid { get; set; }
        public long oid { get; set; }
        public string csrf { get; set; }
        public string otype { get; set; } = "up";
        public string Is_bp_remains_prior { get; set; } = "true";
    }
}
