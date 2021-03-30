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
    public class UserLive : IBiliBili
    {
        public ResultMsg<LiveSignOut> LivSign()
        {
            return HttpMultiClient.HttpMulti.InitCookieContainer()
                .Cookie(BiliBiliOption.Pix + BiliBiliOption.Liv, AppOption.Cookies())
                .AddNode(string.Format(BiliBiliOption.Live, BiliBiliOption.Liv))
                .Header(BiliBiliOption.LivHeader)
                .Build()
                .RunString()
                .FirstOrDefault()
                .ToModel<ResultMsg<LiveSignOut>>();
        }

        public ResultMsg<object> LivPay()
        {
            return HttpMultiClient.HttpMulti.InitCookieContainer()
                 .Cookie(BiliBiliOption.Pix + BiliBiliOption.Liv, AppOption.Cookies())
                 .AddNode(string.Format(BiliBiliOption.LivePay, BiliBiliOption.Liv))
                 .Header(BiliBiliOption.LivHeader)
                 .Build()
                 .RunString()
                 .FirstOrDefault()
                 .ToModel<ResultMsg<object>>();
        }

        public ResultMsg<LiveStatus> LivStatus()
        {
            return HttpMultiClient.HttpMulti.InitCookieContainer()
                .Cookie(BiliBiliOption.Pix + BiliBiliOption.Liv, AppOption.Cookies())
                .AddNode(string.Format(BiliBiliOption.LivStatus, BiliBiliOption.Liv))
                .Header(BiliBiliOption.LivHeader)
                .Build()
                .RunString()
                .FirstOrDefault()
                .ToModel<ResultMsg<LiveStatus>>();
        }
    }
}
