using BetScrapInfo.WebUI.Extensions;
using BetScrapInfo.WebUI.Models;
using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace BetScrapInfo.WebUI.Controllers
{
    public class HomeController : Controller
    {

        IWebHostEnvironment _environment;
        private readonly ILogger<HomeController> _logger;
        private IUrlService _urlService;
        private ITakeMatchCount _takeMatch;
        private static readonly string Mail = "**";
        private static readonly string Pass = "**";

        string wwwPathTxt;
        string contentPath;
        string text;


        public HomeController(ILogger<HomeController> logger, IUrlService urlService,ITakeMatchCount takeMatch, IWebHostEnvironment environment)
        {
            _logger = logger;
            _urlService = urlService;
            _takeMatch = takeMatch;
            _environment = environment;
        }

        public IActionResult Index()
        {

            UrlViewModel model = new UrlViewModel() { UrlList = _urlService.GetList() };

            if (String.IsNullOrEmpty(HttpContext.Session.GetObject<String>("username")))
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return View(model);
            }

        }

        public IActionResult saveUrl(int Id, string Url)
        {

            if (Id != 0 || !String.IsNullOrEmpty(Url))
            {
                var isExcept = _urlService.isExcept(Url);
                if (isExcept == false)
                {
                    return Ok(false);
                }

                var isOk= _urlService.UpdateUrl(Id, Url);
                if (isOk)
                {
                    return Ok(true);

                }
                else
                {
                    return Ok(false);
                }
            }
            else
            {
                return Ok(false);
            }        
        }

        public IActionResult delUrl(int? Id)
        {
            if (Id==0 || Id==null)
            {
                return Ok(false);
             
            }
            else
            {
                var isOk = _urlService.DeleteUrl((int)Id);
                if (isOk == true)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }

            
        }

        public IActionResult addUrl(string Url)
        {
            if (!String.IsNullOrEmpty(Url))
            {
               var isExcept=  _urlService.isExcept(Url);
                if (isExcept==false)
                {
                    return Ok(false);
                }
                var isOk=_urlService.AddUrl(Url);
                if (isOk==true)
                {
                    return Ok(true);
                }
                else
                {
                    return  Ok(false);
                }
            }
            else
            {
                return Ok(false);
            }
   
        }

        [HttpGet]
        [Route("Home/CountOperations/{data}")]
        public JsonResult CountOperations([FromRoute] string data)
        {
            if (data == "d265846-AFx89qd-Sfst20z")
            {
                foreach (var item in _urlService.GetList())
                {

                    // wwwPathTxt = this._environment.WebRootPath + "/errText.txt";
                    // contentPath = this._environment.ContentRootPath;
                    // text = "Durum Kontrol " + DateTime.Now.ToString();

                    //using (StreamWriter sw = System.IO.File.AppendText(wwwPathTxt))
                    //{
                    //    sw.WriteLine(text);
                    //}

                    var count = _takeMatch.GetCount(item.iUrl);
                    if (count == -1) { continue; };

                    if (count == item.Count)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    else if (count > item.Count)
                    {
                        try
                        {
                            var smtpclient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential(Mail, Pass),
                                EnableSsl = true,
                                UseDefaultCredentials=false
                            };
                            int diffrence = count - item.Count;
                            string subj = "son bildiriden sonra ->" + diffrence + " eklendi";
                            string body = item.iUrl;
                            smtpclient.Send(Mail, Mail, subj, body);

                            _urlService.UpdateCount(item.Id, count);
                        }
                        catch (Exception ex)
                        {
                             wwwPathTxt = this._environment.WebRootPath + "/errText.txt";
                             contentPath = this._environment.ContentRootPath;
                             text = ex + "Controller " + DateTime.Now.ToString();

                            using (StreamWriter sw = System.IO.File.AppendText(wwwPathTxt))
                            {
                                sw.WriteLine(text);
                            }
                            continue;
                        }

                    }
                    else if (item.Count > count)
                    {
                        try
                        {
                            var smtpclient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential(Mail, Pass),
                                EnableSsl = true,
                                UseDefaultCredentials = false
                            };
                            string subj = "son bildiriden sonra" + item.Count + "->" + count + " düştü.";
                            string body = item.iUrl;

                            smtpclient.Send(Mail, Mail, subj, body);

                            _urlService.UpdateCount(item.Id, count);
                        }
                        catch (Exception ex)
                        {

                             wwwPathTxt = this._environment.WebRootPath + "/errText.txt";
                             contentPath = this._environment.ContentRootPath;
                             text = ex + "Controller Alt " + DateTime.Now.ToString();

                            using (StreamWriter sw = System.IO.File.AppendText(wwwPathTxt))
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
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
