using DS.Scraping.Models;
using DS.Scraping.Scraping;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Scraping.Scraping.Selenium.Browser
{
    public abstract partial class ABrowser : IBrowserScraping
    {
        #region GetElementSide
        public IWebElement GetElementByXPath(string xPath)
        {
            return _webDriver.FindElement(By.XPath(xPath));
        }

        public IList<IWebElement> GetElementsByXPath(string xPath)
        {
            return _webDriver.FindElements(By.XPath(xPath));
        }

        public IWebElement GetElementByXPath(string xPath, IWebElement webElement)
        {
            return _webDriver.FindElement(By.XPath(xPath));
        }

        public IList<IWebElement> GetElementsByXPath(string xPath, IWebElement webElement)
        {
            return webElement.FindElements(By.XPath(xPath));
        }

        public IWebElement GetElementByTagName(string tagName)
        {
            return _webDriver.FindElement(By.TagName(tagName));
        }

        public IList<IWebElement> GetElementsByTagName(string tagName)
        {
            return _webDriver.FindElements(By.TagName(tagName));
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
            return _webDriver.FindElement(By.ClassName(className));
        }

        public IList<IWebElement> GetElementsByClassName(string className)
        {
            return _webDriver.FindElements(By.ClassName(className));
        }

        public IWebElement GetElementByClassName(string className, IWebElement webElement)
        {
            return webElement.FindElement(By.ClassName(className));
        }

        public IList<IWebElement> GetElementsByClassName(string className, IWebElement webElement)
        {
            return webElement.FindElements(By.ClassName(className));
        }



        #endregion
    }
}
