using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ray.Edna.Option;
using Ray.Edna.TaskCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ray.Web.Hosting
{
    public class TaskHostService : IHostedService
    {
        private readonly ILogger<TaskHostService> log;
        private readonly IHostApplicationLifetime _applicationLifetime;
        //private readonly QuartzService quartz;
        public TaskHostService(ILogger<TaskHostService> logger, IHostApplicationLifetime applicationLifetime)
        {
            log = logger;
            _applicationLifetime = applicationLifetime;
            //quartz = quartzService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            log.LogInformation("this application is begin start");
            new Edna.TaskCore.Jobs.HandleJob().Execute(null);
            _applicationLifetime.StopApplication();
            //quartz.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            log.LogInformation("application  stop");
            return Task.CompletedTask;
        }
    }
}
