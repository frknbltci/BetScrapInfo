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

       
        HtmlNodeCollection match;

        HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
        bool isbottom;

        public TakeMatchCount(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

       public IWebElement waitForPageUntil(By locator,int maxSeconds,IWebDriver driver)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(maxSeconds)
                ).Until(ExpectedConditions.ElementExists((locator)));
        }

        public int GetCount(string Url)
        {
            var wwwPathTxt3 = this._environment.WebRootPath + "/errText.txt";

            var tex3t = Url.ToString();

            using (StreamWriter sw = System.IO.File.AppendText(wwwPathTxt3))
            {
                sw.WriteLine(tex3t.ToString());
            }

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            try
            {
                var cdRunpath = _environment.ContentRootPath;
                
                IWebDriver driver = new ChromeDriver(cdRunpath, chromeOptions);
                try
                {
                    driver.Navigate().GoToUrl(Url);
                    Thread.Sleep(3000);
                    dokuman.LoadHtml(driver.PageSource);
                }
                catch (Exception ex)
                {
                    string wwwPathTxt2 = this._environment.WebRootPath + "/errText.txt";
                    
                    var text = "  /*/*/*/*/Get Count { Url veya loadlanamadı }"+ex + DateTime.Now.ToString();

                    using (StreamWriter sw = File.AppendText(wwwPathTxt2))
                    {
                        sw.WriteLine(text);
                    }


                    driver.Navigate().GoToUrl(Url);

                 
                    Thread.Sleep(4000);
                    dokuman.LoadHtml(driver.PageSource);
                }


                var element=waitForPageUntil(By.XPath("//div[@class='modul-content']//div[contains(@class,'fixture-body flex-container')]"), 5, driver);

                if (element!=null)
                {
                    dokuman.LoadHtml(driver.PageSource);
                }
                else
                {
                    return -1;
                }

                match = dokuman.DocumentNode.SelectNodes("//div[@class='modul-content']//div[contains(@class,'fixture-body flex-container')]");

                HtmlNodeCollection matches = dokuman.DocumentNode.SelectNodes("//div[@class='modul-content']//div[contains(@class,'fixture-body flex-container')]//a[contains(@class,'team-name truncate')]");

               var  wwwPathTxt = this._environment.WebRootPath + "/errText.txt";

                var tex2t = match == null ? 0 : match.Count;

                using (StreamWriter sw = System.IO.File.AppendText(wwwPathTxt))
                {
                    sw.WriteLine(tex2t.ToString());
                }
                foreach (var item in matches)
                {
                    var text =  item.InnerText+ " - " + DateTime.Now.ToString();
                
                    using (StreamWriter sw = System.IO.File.AppendText(wwwPathTxt))
                    {
                        sw.WriteLine(text);
                    }

                }
                //Önceki sayfayı yakalıyor olabilir mi ?
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
                string wwwPathTxt = this._environment.WebRootPath + "/errText.txt";
              
                var text = "<--- Path"+ ex + "Chrome Sıkıntısı" + DateTime.Now.ToString();

                using (StreamWriter sw = File.AppendText(wwwPathTxt))
                {
                    sw.WriteLine(text);
                }
                return -1;
            }
            
        }

        public int GetMobileCount(string Url)
        {
            
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            
            try
            {
                var cdRunpath = _environment.ContentRootPath;
                IWebDriver driver = new ChromeDriver(cdRunpath, chromeOptions);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                try
                {
                    isbottom = dokuman.DocumentNode.SelectSingleNode("//*[@id='allCnt']/app-bottom-menu/div[1]") != null ? true : false;
                    if (isbottom)
                    {
                        var bottom = driver.FindElement(By.XPath("//*[@id='allCnt']/app-bottom-menu/div[1]"));
                        js.ExecuteScript("arguments[0].style.display = 'none';", bottom);
                    }

                    driver.Navigate().GoToUrl(Url);
                    Thread.Sleep(3000);
                    dokuman.LoadHtml(driver.PageSource);
                }
                catch (Exception ex)
                {
                    string wwwPathTxt = this._environment.WebRootPath + "/errText.txt";

                    var text = "  /*/*/*/*/Get Count { Url veya loadlanamadı }" + ex + DateTime.Now.ToString();

                    using (StreamWriter sw = File.AppendText(wwwPathTxt))
                    {
                        sw.WriteLine(text);
                    }

                    Thread.Sleep(1000);
                    driver.Navigate().GoToUrl(Url);
                    Thread.Sleep(4000);
                    dokuman.LoadHtml(driver.PageSource);
                }

                match = dokuman.DocumentNode.SelectNodes("//div[@class='fixture-container']//div[contains(@class,'match-content')]");

                driver.Close();
                driver.Quit();

                if (match != null)
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
                string wwwPathTxt = this._environment.WebRootPath + "/errText.txt";

                var text = "<--- Path" + ex + "Chrome Sıkıntısı" + DateTime.Now.ToString();

                using (StreamWriter sw = File.AppendText(wwwPathTxt))
                {
                    sw.WriteLine(text);
                }
                return -1;
            }
        }
    }
}
