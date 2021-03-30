using Ray.Edna.BiliBili.Input;
using Ray.Edna.BiliBili.Output;
using Ray.Edna.Option;
using Synctool.HttpFramework;
using Synctool.LinqFramework;
using System.Linq;

namespace Ray.Edna.BiliBili.All
{
    public class UserCharge : IBiliBili
    {
        public ResultMsg<ChargeOut> Charge(ChargeIn info)
        {
            return HttpMultiClient.HttpMulti.InitCookieContainer()
                .Cookie(BiliBiliOption.Pix + BiliBiliOption.Api, AppOption.Cookies())
                .AddNode(string.Format(BiliBiliOption.Charge, BiliBiliOption.Api), info, Type: RequestType.POST)
                .Header(BiliBiliOption.Header).Build().RunString()
                .FirstOrDefault().ToModel<ResultMsg<ChargeOut>>();
        }
    }
}
