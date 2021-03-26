using Microsoft.Extensions.Logging;
using Quartz;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.Edna.TaskCore.Jobs
{
    public class HandleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
             Log.Information(context.JobDetail.Key.Name);
            return Task.CompletedTask;
        }
    }
}
