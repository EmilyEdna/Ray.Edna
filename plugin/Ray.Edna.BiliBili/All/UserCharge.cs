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
            var Dic = AppOption.Cookies();
            Dic.Remove("bsource");
            var cookies = HttpMultiClient.HttpMulti.InitCookieContainer()
                            .AddNode(string.Format(BiliBiliOption.Charge, BiliBiliOption.Api), info, Type: RequestType.POST)
                            .Cookie("bsource", "search_baidu", "/", BiliBiliOption.Api);
            Dic.ForDicEach((key, value) =>
            {
                cookies = cookies.Cookie(key, value, "/", BiliBiliOption.Api);
            });

            return cookies
                .Header("Referer", "https://www.bilibili.com/")
                .Header("Origin", "https://www.bilibili.com/")
                .Build().RunString().FirstOrDefault()
                .ToModel<ResultMsg<ChargeOut>>();
        }
    }
}
