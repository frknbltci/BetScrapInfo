using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace BetScrapInfo.WebUI.Extensions
{
    public class TakeMatchCount: ITakeMatchCount
    {
        private IWebHostEnvironment _environment;


        public TakeMatchCount(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        HtmlNodeCollection match;

        HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();

        
        public int GetCount(string Url)
        {
           
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
        
            try
            {
                var cdRunpath = _environment.ContentRootPath;
                IWebDriver driver = new ChromeDriver(cdRunpath, chromeOptions);
                try
                {
                    driver.Navigate().GoToUrl(Url);
                    Thread.Sleep(2000);
                    dokuman.LoadHtml(driver.PageSource);
                }
                catch (Exception)
                {
                    driver.Navigate().GoToUrl(Url);
                    Thread.Sleep(4000);
                    dokuman.LoadHtml(driver.PageSource);
                }
                
            
                match = dokuman.DocumentNode.SelectNodes("//div[@class='modul-content']//div[contains(@class,'fixture-body flex-container')]");

                driver.Close();
                driver.Quit();

                if (match !=null)
                {
                    return match.Count;
                }
                else
                {
                    return -1;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -1;
            }
            
        }
    }
}
