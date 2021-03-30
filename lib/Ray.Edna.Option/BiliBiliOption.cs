using System.Collections.Concurrent;

namespace Ray.Edna.Option
{
    public class BiliBiliOption
    {
        public const string Api = "api.bilibili.com";
        public const string CookieLogin = "https://{0}/x/web-interface/nav";
        public const string Vip = "https://{0}/x/vip/privilege/receive";
        public const string Charge = "https://{0}/x/ugcpay/web/v2/trade/elec/pay/quick";

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
