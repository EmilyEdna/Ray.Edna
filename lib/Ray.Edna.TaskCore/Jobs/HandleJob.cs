using Quartz;
using Ray.Edna.BiliBili.All;
using Ray.Edna.BiliBili.Input;
using Ray.Edna.Option;
using Serilog;
using Synctool.LinqFramework;
using System;
using System.Threading.Tasks;

namespace Ray.Edna.TaskCore.Jobs
{
    public class HandleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            #region 用户信息
            var data = BiliBiliOption.Get<UserAssest>(nameof(UserAssest)).CookieLogin();

            Log.Information("每日登录 经验+{0} {1}", 5, "√");
            Log.Information("用户名：{0}", data.Uname);
            Log.Information("硬币：{0}", data.Money ?? 0);

            if (data.Level_info.Current_level < 6)
            {
                Log.Information("如每日做满65点经验，距离升级到 Lv{0} 还有: {1}天",
                    data.Level_info.Current_level + 1,
                    (data.Level_info.GetNext_expLong() - data.Level_info.Current_exp) / 65);
            }
            else
            {
                Log.Information("您已是 Lv6 的大佬了，当前经验：{0}，无敌是多么寂寞~", data.Level_info.Current_exp);
            }
            #endregion

            #region 会员权益
            //todo reason ：只有年费才执行
            var viptype = data.GetVipType();
            if (viptype == 2 && AppOption.MoneyGetTime == DateTime.Now.Day)
            {
                var result = BiliBiliOption.Get<UserVip>(nameof(UserVip)).GetVipPrivilege(new VipIn
                {
                    csrf = AppOption.Cookies()["bili_jct"],
                    type = 1
                });

                if (result.Code == 0)
                    Log.Information("领取年度大会员每月赠送的B币券成功 {0}", "√");
                else
                    Log.Error("领取失败，失败原因：{0} 错误码：{1} {2}", result.Message, result.Code, "×");
            }

            #endregion

            return Task.CompletedTask;
        }
    }
}
