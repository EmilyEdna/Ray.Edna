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
            return HttpMultiClient.HttpMulti.InitCookieContainer()
                  .Cookie(BiliBiliOption.Pix + BiliBiliOption.Api, AppOption.Cookies())
                  .AddNode(string.Format(BiliBiliOption.Vip, BiliBiliOption.Api), input, Type: RequestType.POST)
                  .Build().RunString().FirstOrDefault().ToModel<ResultMsg<object>>();
        }
    }
}
