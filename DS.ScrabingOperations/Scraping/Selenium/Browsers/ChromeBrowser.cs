using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium.Browsers
{
    public class ChromeBrowser : IBrowser<ChromeDriver>
    {
        public ChromeDriver driver = null;

        public ChromeBrowser()
        {
            driver = new ChromeDriver(GetDriverService(), GetBrowserOptions());
        }

        public ChromeDriver GetDriver()
        {
            return driver;
        }

        public ChromeDriver GetDriver(string url)
        {
            driver.Navigate().GoToUrl(url);
            return driver;
        }
        public ChromeDriver GetDriver(string url, int waitSecond)
        {
            driver.Navigate().GoToUrl(url);
            WaitWhileReachingUrl(waitSecond);
            return driver;
        }

        private ChromeDriverService GetDriverService()
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            return service;
        }

        public void WaitWhileReachingUrl(int second)
        {
            Thread.Sleep(second * 1000);
        }

        private ChromeOptions GetBrowserOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("--window-position=-32000,-32000");

            return options;
        }
        public void CloseDriver()
        {
            driver.Close();
        }

       
    }
}
