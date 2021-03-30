using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ray.Edna.Option
{
    public class BiliBiliOption
    {
        public const string Pix = "https://";
        public const string Api = "api.bilibili.com";
        public const string Liv = "api.live.bilibili.com";
        public const string CookieLogin = "https://{0}/x/web-interface/nav";
        public const string Vip = "https://{0}/x/vip/privilege/receive";
        public const string Charge = "https://{0}/x/ugcpay/web/v2/trade/elec/pay/quick";
        public const string Live = "https://{0}/xlive/web-ucenter/v1/sign/DoSign";
        public const string LivePay = "https://{0}/pay/v1/Exchange/silver2coin";
        public const string LivStatus = "https://{0}/pay/v1/Exchange/getStatus";
        public const string Video = "https://{0}/x/relation/followings";
        public const string VideoSearch = "https://{0}/x/space/arc/search";

        public static Dictionary<string, string> DefaultHeader = new Dictionary<string, string>
        {
            { "Accept","application/json, text/plain, */*"},
            { "Accept-Encoding","deflate, br"},
            { "Accept-Language","zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"},
            { "Sec-Fetch-Dest","empty"},
            { "Sec-Fetch-Mode","cors"},
            { "Sec-Fetch-Site","same-site"},
            { "Connection","keep-alive"}
        };

        public static Dictionary<string, string> Header = new Dictionary<string, string>
        {
            { "Referer","https://www.bilibili.com/"},
            { "Origin","https://www.bilibili.com/"}
        };

        public static Dictionary<string, string> LivHeader = new Dictionary<string, string> {
             { "Referer","https://link.bilibili.com/"},
             { "Origin","https://link.bilibili.com/"}
        };

        #region Mini IOC
        private static ConcurrentDictionary<string, object> Ioc = new();
        public static void Set(string key, object obj)
        {
            if (!Ioc.ContainsKey(key))
                Ioc.TryAdd(key, obj);
        }
        public static T Get<T>(string key)
        {
            if (Ioc.ContainsKey(key))
                return (T)Ioc[key];
            else return default;
        }
        #endregion
    }
}
