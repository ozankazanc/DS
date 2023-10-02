using DS.ScrabingOperations.Scraping.Selenium.Browsers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium
{
    public class SeleniumScraping : IScraping
    {

        private IWebDriver _driver;
        private ChromeBrowser _chromeBrowser;
        private string _url;
        public SeleniumScraping(string url)
        {
            _url = url;
            _chromeBrowser = new ChromeBrowser();
            _driver = _chromeBrowser.GetDriver(url, 5);
        }

        public IWebElement GetElementByXPath(string xPath)
        {
            return _driver.FindElement(By.XPath(xPath));
        }

        public IList<IWebElement> GetElementsByXPath(string xPath)
        {
            return _driver.FindElements(By.XPath(xPath));
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
            return _driver.FindElement(By.TagName(tagName));
        }

        public IList<IWebElement> GetElementsByTagName(string tagName)
        {
            return _driver.FindElements(By.TagName(tagName));
        }

        public IWebElement GetElementByTagName(string tagName, IWebElement webElement)
        {
            return webElement.FindElement(By.TagName(tagName));
        }

        public IList<IWebElement> GetElementsByTagName(string tagName, IWebElement webElement)
        {
            return webElement.FindElements(By.TagName(tagName));
        }
        public void StopWrobser()
        {
            _chromeBrowser.CloseDriver();
        }
        public void RestartWrobser()
        {
            _chromeBrowser.CloseDriver();
            _driver = _chromeBrowser.GetDriver(_url, 5);
        }

    }
}
