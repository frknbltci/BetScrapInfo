using Microsoft.AspNetCore.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BetScrapInfo.WebUI.BaseQuartz
{
    public class QuartzJobFactory : IJobFactory
    {

        private readonly IServiceProvider _serviceProvider;
        private IWebHostEnvironment _environment;
        public QuartzJobFactory(IServiceProvider serviceProvider, IWebHostEnvironment environment)
        {
            _serviceProvider = serviceProvider;
            _environment = environment;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;

            var job = (IJob)_serviceProvider.GetService(jobDetail.JobType);

            return job;
        }

        public void ReturnJob(IJob job)
        {
            var x= new NotImplementedException();

            string wwwPathTxt = this._environment.WebRootPath + "/errText.txt";

            var text = "***********" + x;

            using (StreamWriter sw = File.AppendText(wwwPathTxt))
            {
                sw.WriteLine(text);
            }

        }
    }
}
