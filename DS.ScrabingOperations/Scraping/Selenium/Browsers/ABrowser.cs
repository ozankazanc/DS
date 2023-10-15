using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium.Browsers
{
    internal abstract class ABrowser<TWebDriver, TDriverService, TOptions>
        where TWebDriver : WebDriver
    {
        private TWebDriver Driver { get; set; }
        protected abstract TWebDriver SetWebDriver();
        protected abstract TDriverService SetDriverSevice();
        protected abstract TOptions SetOptions();
        public virtual void WaitWhileReachingUrl(int milisecond)
        {
            Thread.Sleep(milisecond);
        }
        public virtual TWebDriver SetCustomWebDriver()
        {
            throw new NotImplementedException();
        }

        public IWebDriver GetDriver()
        {
            return Driver;
        }
        public IWebDriver GetDriver(string url)
        {
            return GetDriver(url, 1000);
        }
        public IWebDriver GetDriver(string url, int waitMiliSecond)
        {
            return GetDriver(url, waitMiliSecond, false);
        }
        public IWebDriver GetDriver(string url, int waitMiliSecond, bool useLocalBrowser)
        {
            if (useLocalBrowser)
                Driver = SetCustomWebDriver();
            else
                Driver = SetWebDriver();
            
            Driver.Url = url;
            WaitWhileReachingUrl(waitMiliSecond);
           
            return Driver;
        }

        public void CloseDriver()
        {
            Driver.Quit();
        }

    }
}
