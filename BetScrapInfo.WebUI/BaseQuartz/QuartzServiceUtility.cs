using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetScrapInfo.WebUI.BaseQuartz
{
    public class QuartzServiceUtility
    {
        public static void StartJob<TJob>(IScheduler scheduler,TimeSpan timeSpan) where TJob : IJob
        {
            var jobName = typeof(TJob).FullName;

            var job = JobBuilder.Create<TJob>().WithIdentity(jobName).Build();

            var trigger = TriggerBuilder.Create().WithIdentity($"{jobName}.trigger").StartNow()
                .WithSimpleSchedule(scheduleBuilder =>
                    scheduleBuilder.WithInterval(timeSpan).RepeatForever())
                .Build();
            scheduler.ScheduleJob(job, trigger);

        }

    }
}
