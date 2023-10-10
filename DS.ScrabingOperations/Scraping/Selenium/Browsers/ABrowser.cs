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
    public abstract class ABrowser<TWebDriver, TDriverService, TOptions> : IScraping
        where TWebDriver : WebDriver
    {
        public ABrowser()
        {
            
            
        }
        protected virtual TWebDriver Driver => SetWebDriver();
        protected abstract TWebDriver SetWebDriver();
        protected abstract TDriverService SetDriverSevice();
        protected abstract TOptions SetOptions();
        public virtual void WaitWhileReachingUrl(int milisecond)
        {
            Thread.Sleep(milisecond);
        }

        protected IWebDriver GetDriver()
        {
            return Driver;
        }
        protected IWebDriver GetDriver(string url)
        {
            Driver.Navigate().GoToUrl(url);
            return Driver;
        }
        protected IWebDriver GetDriver(string url, int waitSecond)
        {
            Driver.Navigate().GoToUrl(url);
            return Driver;
        }
       
        public void CloseDriver()
        {
            _driver.Close();
        }

        public IWebElement GetElementByXPath(string xPath)
        {
            return Driver.FindElement(By.XPath(xPath));
        }

        public IList<IWebElement> GetElementsByXPath(string xPath)
        {
            return Driver.FindElements(By.XPath(xPath));
        }

        public IWebElement GetElementByXPath(string xPath, IWebElement webElement)
        {
            return Driver.FindElement(By.XPath(xPath));
        }

        public IList<IWebElement> GetElementsByXPath(string xPath, IWebElement webElement)
        {
            return webElement.FindElements(By.XPath(xPath));
        }

        public IWebElement GetElementByTagName(string tagName)
        {
            return Driver.FindElement(By.TagName(tagName));
        }

        public IList<IWebElement> GetElementsByTagName(string tagName)
        {
            return Driver.FindElements(By.TagName(tagName));
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
            return Driver.FindElement(By.ClassName(className));
        }

        public IList<IWebElement> GetElementsByClassName(string className)
        {
            return Driver.FindElements(By.ClassName(className));
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
