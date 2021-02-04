using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetScrapInfo.WebUI.Extensions
{
    public class TakeMatchCount
    {

        HtmlNodeCollection match;
       
        HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();

        public int GetCount(string Url)
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
        
            try
            {
                IWebDriver driver = new ChromeDriver(@"C:\Program Files (x86)\Google\Update",chromeOptions);
                driver.Navigate().GoToUrl(Url);
                Thread.Sleep(5000);
                dokuman.LoadHtml(driver.PageSource);
            
                match = dokuman.DocumentNode.SelectNodes("//div[@class='modul-content']//div[contains(@class,'fixture-body flex-container')]");

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
