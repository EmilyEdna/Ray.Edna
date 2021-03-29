using Newtonsoft.Json.Linq;
using Ray.Edna.BiliBili.Input;
using Ray.Edna.BiliBili.Output;
using Ray.Edna.Option;
using Synctool.HttpFramework;
using Synctool.LinqFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.Edna.BiliBili.All
{
    public class UserVip : IBiliBili
    {
        public ResultMsg<object> GetVipPrivilege(VipIn input) 
        {
            var Dic = AppOption.Cookies();
            Dic.Remove("bsource");
            var cookies = HttpMultiClient.HttpMulti.InitCookieContainer()
                            .AddNode(string.Format(BiliBiliOption.Vip, BiliBiliOption.Api), input,Type:RequestType.POST)
                            .Cookie("bsource", "search_baidu", "/", BiliBiliOption.Api);
            Dic.ForDicEach((key, value) =>
            {
                cookies = cookies.Cookie(key, value, "/", BiliBiliOption.Api);
            });

            return cookies.Build().RunString().FirstOrDefault().ToModel<ResultMsg<object>>();
        }
    }
}
