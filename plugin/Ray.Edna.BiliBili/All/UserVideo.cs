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
    public class UserVideo : IBiliBili
    {
        public ResultMsg<VideoFollowingOut> GetFollowings(VideoIn input)
        {
            return HttpMultiClient.HttpMulti.InitCookieContainer()
                             .AddNode(string.Format(BiliBiliOption.Video, BiliBiliOption.Api), input)
                             .Cookie(BiliBiliOption.Pix + BiliBiliOption.Api, AppOption.Cookies())
                             .Header("Referer", "https://space.bilibili.com/")
                             .Build().RunString().FirstOrDefault()
                             .ToModel<ResultMsg<VideoFollowingOut>>();
        }

        public UpVideoInfo GetRandomVideoOfUps(List<long> input)
        {
            long upId = input[new Random().Next(0, input.Count)];

            return HttpMultiClient.HttpMulti.InitCookieContainer()
                              .AddNode(string.Format(BiliBiliOption.VideoSearch, BiliBiliOption.Api), new VideoSearch { Mid = upId })
                              .Cookie(BiliBiliOption.Pix + BiliBiliOption.Api, AppOption.Cookies())
                              .Header("Referer", "https://space.bilibili.com/")
                              .Header("Origin", "https://space.bilibili.com/")
                              .Build().RunString().FirstOrDefault()
                              .ToModel<ResultMsg<SearchUpVideosResponse>>().Data.List.Vlist.FirstOrDefault();

        }

        public ResultMsg<object> UpVideo(UpVideoWatch input)
        {
            return HttpMultiClient.HttpMulti.InitCookieContainer()
                                 .AddNode(string.Format(BiliBiliOption.UpVideo, BiliBiliOption.Api, input.Aid, input.Played_time), input, Type: RequestType.POST)
                                 .Cookie(BiliBiliOption.Pix + BiliBiliOption.Api, AppOption.Cookies())
                                 .Header(BiliBiliOption.Header)
                                 .Build().RunString().FirstOrDefault()
                                 .ToModel<ResultMsg<object>>();
        }

        public ResultMsg<object> ShareVideo(ShareVideo input)
        {
            return HttpMultiClient.HttpMulti.InitCookieContainer()
                                .AddNode(string.Format(BiliBiliOption.ShareVideo, BiliBiliOption.Api), input, Type: RequestType.POST)
                                .Cookie(BiliBiliOption.Pix + BiliBiliOption.Api, AppOption.Cookies())
                                .Header(BiliBiliOption.Header)
                                .Build().RunString().FirstOrDefault()
                                .ToModel<ResultMsg<object>>();
        }
    }
}
