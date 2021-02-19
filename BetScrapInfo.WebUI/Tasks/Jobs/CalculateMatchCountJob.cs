using BetScrapInfo.WebUI.Extensions;
using Business.Abstract;
using Microsoft.AspNetCore.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace BetScrapInfo.WebUI.Tasks.Jobs
{
    public class CalculateMatchCountJob : IJob
    {

        private IWebHostEnvironment environment;
        private IUrlService _urlService;
        private ITakeMatchCount _takeMatch;
        private static readonly string Mail = "**";
        private static readonly string Pass = "*";

        public CalculateMatchCountJob(IWebHostEnvironment _environment, IUrlService urlService,ITakeMatchCount takeMatch)
        {
            _urlService = urlService;
            _takeMatch = takeMatch;
            environment = _environment;
           
        }
      async public  Task Execute(IJobExecutionContext context)
        {
            try
            {
                //string wwwPathTxt = this.environment.WebRootPath + "/errText.txt";
                //string contentPath = this.environment.ContentRootPath;
                //var text = "Çalışıyormusun ?" + DateTime.Now.ToString();

                //using (StreamWriter sw = File.AppendText(wwwPathTxt))
                //{
                //    sw.WriteLine(text);
                //}

                //CountOperations();
            }
            catch (Exception ex)
            {
                
                string wwwPathTxt = this.environment.WebRootPath + "/errText.txt";
                string contentPath = this.environment.ContentRootPath;
                var text = ex + DateTime.Now.ToString();

                using (StreamWriter sw = File.AppendText(wwwPathTxt))
                {
                    sw.WriteLine(text);
                }
            }

             await Task.FromResult(0);
        }

        public void CountOperations()
        {
        
            foreach (var item in _urlService.GetList())
            {

                var url = _urlService.GetById(item.Id);

                var count = _takeMatch.GetCount(url.iUrl);

               
                if (count == -1) { continue; };

                if (count == url.Count)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                else if (count > url.Count)
                {
                    try
                    {
                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(Mail, Pass),
                            EnableSsl = true,
                        };
                        int diffrence = count - url.Count;
                        string subj = "Son Bildiriden sonra ->" + diffrence + " Eklendi";
                        string body = item.iUrl;
                        smtpClient.Send(Mail, Mail, subj, body);

                        _urlService.UpdateCount(item.Id, count);
                    }
                    catch (Exception ex)
                    {
                        string wwwPathTxt = this.environment.WebRootPath + "/errText.txt";
                        string contentPath = this.environment.ContentRootPath;
                        var text = ex +"Mail Gönderimi sıkıntısı" + DateTime.Now.ToString();

                        using (StreamWriter sw = File.AppendText(wwwPathTxt))
                        {
                            sw.WriteLine(text);
                        }

                        continue;
                    }

                }
                else if (url.Count > count)
                {
                    try
                    {
                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(Mail, Pass),
                            EnableSsl = true,
                        };
                        string subj = "Son Bildiriden sonra" + url.Count + "->" + count + " düştü.";
                        string body = url.iUrl;



                        smtpClient.Send(Mail, Mail, subj, body);

                        _urlService.UpdateCount(item.Id, count);
                    }
                    catch (Exception ex)
                    {

                        string wwwPathTxt = this.environment.WebRootPath + "/errText.txt";
                        string contentPath = this.environment.ContentRootPath;
                        var text = ex + "Mail Gönderimi sıkıntısı" + DateTime.Now.ToString();

                        using (StreamWriter sw = File.AppendText(wwwPathTxt))
                        {
                            sw.WriteLine(text);
                        }
                        continue;
                    }
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
