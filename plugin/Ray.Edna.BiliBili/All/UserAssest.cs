using Newtonsoft.Json.Linq;
using Ray.Edna.BiliBili.Output;
using Ray.Edna.Option;
using Synctool.HttpFramework;
using Synctool.LinqFramework;
using System.Linq;

namespace Ray.Edna.BiliBili.All
{
    public class UserAssest : IBiliBili
    {
        public UserInfo CookieLogin()
        {
            var Dic = AppOption.Cookies();
            Dic.Remove("bsource");
            var cookies = HttpMultiClient.HttpMulti.InitCookieContainer()
                            .AddNode(string.Format(BiliBiliOption.CookieLogin, BiliBiliOption.Api)).Cookie("bsource", "search_baidu", "/", BiliBiliOption.Api);
            Dic.ForDicEach((key, value) =>
            {
                cookies = cookies.Cookie(key, value, "/", BiliBiliOption.Api);
            });

            return cookies.Build().RunString().FirstOrDefault()
                .ToModel<JObject>()
                .SelectToken("data")
                .ToJson()
                .ToModel<UserInfo>();
        }
    }
}
