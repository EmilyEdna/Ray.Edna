using Quartz;
using Ray.Edna.BiliBili.Account;
using Ray.Edna.Option;
using Serilog;
using Synctool.LinqFramework;
using System.Threading.Tasks;

namespace Ray.Edna.TaskCore.Jobs
{
    public class HandleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var data = BiliBiliOption.Get<UserAssest>(nameof(UserAssest)).CookieLogin();
            Log.Information(data.ToJson());
            return Task.CompletedTask;
        }
    }
}
