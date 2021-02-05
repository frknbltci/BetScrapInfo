using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BetScrapInfo.WebUI.BaseQuartz
{
    public class QuartzJobFactory : IJobFactory
    {

        private readonly IServiceProvider _serviceProvider;

        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;

            var job = (IJob)_serviceProvider.GetService(jobDetail.JobType);

            return job;
        }

        public void ReturnJob(IJob job)
        {
            throw new NotImplementedException();
        }
    }
}
