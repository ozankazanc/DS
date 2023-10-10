using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium.Browsers
{
    internal class FirefoxBrowser : IBrowser<FirefoxDriver>, IScraping
    {
        public FirefoxDriver driver = null;

        public FirefoxBrowser()
        {
            //driver = new ChromeDriver(GetDriverService(), GetBrowserOptions());
            driver = new FirefoxDriver(GetDriverService());
        }

        public FirefoxDriver GetDriver()
        {
            return driver;
        }

        public FirefoxDriver GetDriver(string url)
        {
            driver.Navigate().GoToUrl(url);
            return driver;
        }

        public FirefoxDriver GetDriver(string url, int waitSecond)
        {
            driver.Navigate().GoToUrl(url);
            WaitWhileReachingUrl(waitSecond);
            return driver;
        }

        private FirefoxDriverService GetDriverService()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
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
      
        public IWebElement GetElementByXPath(string xPath)
        {
            return driver.FindElement(By.XPath(xPath));
        }

        public IList<IWebElement> GetElementsByXPath(string xPath)
        {
            return driver.FindElements(By.XPath(xPath));
        }

        public IWebElement GetElementByXPath(string xPath, IWebElement webElement)
        {
            return webElement.FindElement(By.XPath(xPath));
        }

        public IList<IWebElement> GetElementsByXPath(string xPath, IWebElement webElement)
        {
            return webElement.FindElements(By.XPath(xPath));
        }

        public IWebElement GetElementByTagName(string tagName)
        {
            return driver.FindElement(By.TagName(tagName));
        }

        public IList<IWebElement> GetElementsByTagName(string tagName)
        {
            return driver.FindElements(By.TagName(tagName));
        }

        public IWebElement GetElementByTagName(string tagName, IWebElement webElement)
        {
            return webElement.FindElement(By.TagName(tagName));
        }

        public IList<IWebElement> GetElementsByTagName(string tagName, IWebElement webElement)
        {
            return webElement.FindElements(By.TagName(tagName));
        }

        public IWebElement GetElementByClassName(string className)
        {
            return driver.FindElement(By.ClassName(className));
        }

        public IList<IWebElement> GetElementsByClassName(string className)
        {
            return driver.FindElements(By.ClassName(className));
        }

        public IWebElement GetElementByClassName(string className, IWebElement webElement)
        {
            return webElement.FindElement(By.ClassName(className));
        }

        public IList<IWebElement> GetElementsByClassName(string className, IWebElement webElement)
        {
            return webElement.FindElements(By.ClassName(className));
        }
    }
}
