using DS.Scraping.Scraping;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.Scraping.Scraping.Selenium.Browser
{
    public abstract partial class ABrowser
    {
        protected IWebDriver _webDriver;
        private const int STANDARTWAITINGTIME_WHENREACHINGURL = 1000;
        protected abstract DriverService SetDriverService();
        protected abstract DriverOptions SetOptions();
        public abstract void RunBrowser();
        public virtual void WaitWhileReachingUrl(int milisecond)
        {
            Thread.Sleep(milisecond);
        }
        public void CloseBrowser()
        {
            _webDriver.Quit();
        }
        public void GoToUrl(string url)
        {
            GoToUrl(url, STANDARTWAITINGTIME_WHENREACHINGURL);
        }
        public void GoToUrl(string url, int waitingTimeMs)
        {
            _webDriver.Navigate().GoToUrl(url);
            WaitWhileReachingUrl(waitingTimeMs);
        }


    }

}
