using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.Edna.Option
{
    public class AppOption
    {
        public static Dictionary<string, string> Jobs { get; set; }

        public static string CookieStr { get; set; }

        public static int MoneyDay { get; set; }

        public static string ChargeUp { get; set; }

        public static List<string> Charge { get; set; }

        public static Dictionary<string, string> Cookies()
        {
            Dictionary<string, string> dic = new();
            CookieStr.Split(";").ToList().ForEach(t =>
            {
                dic.Add(t.Split("=")[0], t.Split("=")[1]);
            });
            return dic;
        }
    }
}
