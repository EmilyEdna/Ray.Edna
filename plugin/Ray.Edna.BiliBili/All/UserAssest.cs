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
            return HttpMultiClient.HttpMulti.InitCookieContainer()
                            .AddNode(string.Format(BiliBiliOption.CookieLogin, BiliBiliOption.Api))
                            .Cookie(BiliBiliOption.Pix + BiliBiliOption.Api, AppOption.Cookies())
                            .Build().RunString().FirstOrDefault()
                            .ToModel<JObject>().SelectToken("data")
                            .ToJson().ToModel<UserInfo>();
        }
    }
}
