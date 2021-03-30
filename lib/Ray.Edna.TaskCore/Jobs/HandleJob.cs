using Quartz;
using Ray.Edna.BiliBili.All;
using Ray.Edna.BiliBili.Input;
using Ray.Edna.Option;
using Serilog;
using Synctool.LinqFramework;
using System;
using System.Linq;
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
            if (viptype == 2 && AppOption.MoneyDay == DateTime.Now.Day)
            {
                //领取B币
                var coupon = BiliBiliOption.Get<UserVip>(nameof(UserVip)).GetVipPrivilege(new VipIn
                {
                    csrf = AppOption.Cookies()["bili_jct"],
                    type = 1
                });

                if (coupon.Code == 0)
                    Log.Information("领取年度大会员每月赠送的B币券成功 {0}", "√");
                else
                    Log.Error("领取失败，失败原因：{0} 错误码：{1} {2}", coupon.Message, coupon.Code, "×");

                //其他权益
                var othercoupon = BiliBiliOption.Get<UserVip>(nameof(UserVip)).GetVipPrivilege(new VipIn
                {
                    csrf = AppOption.Cookies()["bili_jct"],
                    type = 2
                });

                if (othercoupon.Code == 0)
                    Log.Information("领取大会员福利/权益 {0}", "√");
                else
                    Log.Error("领取失败，失败原因：{0} 错误码：{1} {2}", othercoupon.Message, othercoupon.Code, "×");
            }

            #endregion

            #region 充电
            var auto = bool.Parse(AppOption.Charge.FirstOrDefault().ToLower());
            if (!auto)
            {
                Log.Information("已配置为不进行自动充电，跳过充电任务");
                return Task.CompletedTask;
            }
            var days = int.Parse(AppOption.Charge.LastOrDefault());
            if (days != -1)
            {
                Log.Information("目标充电日期为{targetDay}号，今天是{today}号，跳过充电任务", days, DateTime.Today.Day);
                return Task.CompletedTask;
            }
            if (data.Wallet.Coupon_balance < 2)
            {
                Log.Information("B币小于2，无法充电");
                return Task.CompletedTask;
            }
            if (viptype != 2)
            {
                Log.Information("不是年度大会员或已过期，不进行B币券自动充电");
                return Task.CompletedTask;
            }
            string targetId = AppOption.ChargeUp;
            if (AppOption.ChargeUp.IsNullOrEmpty())
                targetId = data.Mid.ToString();

            var charge = BiliBiliOption.Get<UserCharge>(nameof(UserCharge)).Charge(new ChargeIn
            {
                bp_num = data.Wallet.Coupon_balance,
                oid = long.Parse(targetId),
                up_mid = long.Parse(targetId),
                csrf = AppOption.Cookies()["bili_jct"]
            });
            if (charge.Code == 0)
            {
                if (charge.Data.Status == 4)
                {
                    Log.Information("充电成功，经验+{exp} √", data.Wallet.Coupon_balance);
                    Log.Information("本次为{upId}充值了: {num}个B币，送的B币券没有浪费哦", targetId, data.Wallet.Coupon_balance);
                }
                else
                {
                    Log.Information("充电失败了啊 原因：{reason}", charge.ToJson());
                }
            }
            else
            {
                Log.Information("充电失败了啊 原因：{reason}", charge.Message);
            }
            #endregion

            return Task.CompletedTask;
        }
    }
}
