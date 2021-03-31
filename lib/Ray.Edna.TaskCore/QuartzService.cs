using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Synctool.StaticFramework;
using Synctool.LinqFramework;
using System.Collections.ObjectModel;
using Ray.Edna.Option;

namespace Ray.Edna.TaskCore
{
    public class QuartzService
    {
        private Task<IScheduler> scheduler;
        public QuartzService()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler();
        }

        public Task Start()
        {
            IScheduler sch = scheduler.Result;

            IDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> dic = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
            List<ITrigger> triggers = new();

            SyncStatic.Assembly(this.GetType().Assembly.GetName().Name)
               .SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(IJob)))).ForEnumerEach(item =>
               {
                   IJobDetail detail = JobBuilder.Create(item).WithIdentity(item.Name, item.FullName).Build();
                   AppOption.Jobs.TryGetValue(item.Name, out string Cron);
                   ITrigger trigger = TriggerBuilder.Create().WithCronSchedule(Cron).Build();
                   
                   triggers.Add(trigger);
                   dic.Add(detail, triggers.AsReadOnly());
               });

            IReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>> pairs = new ReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>>(dic);

            sch.ScheduleJobs(pairs, true);

            return sch.Start();
        }
    }
}
