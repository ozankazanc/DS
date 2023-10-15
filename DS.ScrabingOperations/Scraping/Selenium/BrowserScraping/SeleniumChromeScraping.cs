using DS.ScrabingOperations.Models;
using DS.ScrabingOperations.Scraping.Selenium.Browsers;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V114.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium.BrowserScraping
{
    public class SeleniumChromeScraping
    {
        private ISeleniumBrowserScraping _seleniumBrowserScraping;
        private ChromeBrowser _browser;
        public SeleniumChromeScraping()
        {
            _browser = new ChromeBrowser();
        }

        public void GoToUrl(string url)
        {
            GoToUrl(url, false);
        }
        public void GoToUrl(string url, bool useLocalBrowser)
        {
            _seleniumBrowserScraping = new SeleniumBrowserScraping(_browser.GetDriver(url, 1000, useLocalBrowser));
        }

        public void ClickNextPage()
        {
           
        }

        public DataTable GetData(DataInformation dataInformation)
        {
            return _seleniumBrowserScraping.GetData(dataInformation);
        }
        public void CloseBrowser()
        {
            _browser.CloseDriver();
        }

        public void ScrollDown()
        {
            long scrollHeight = 0;

            do
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_browser.GetDriver();
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                if (newScrollHeight == scrollHeight)
                {
                    break;
                }
                else
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(400);
                }
            } while (true);
        }

    }
}
