using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Scraping.Scraping.Selenium.Browser
{
    internal interface IBrowserScraping
    {
        //xpath
        IWebElement GetElementByXPath(string xPath);
        IWebElement GetElementByXPath(string xPath, IWebElement webElement);
        IList<IWebElement> GetElementsByXPath(string xPath);
        IList<IWebElement> GetElementsByXPath(string xPath, IWebElement webElement);
        //tagname
        IWebElement GetElementByTagName(string tagName);
        IWebElement GetElementByTagName(string tagName, IWebElement webElement);
        IList<IWebElement> GetElementsByTagName(string tagName);
        IList<IWebElement> GetElementsByTagName(string tagName, IWebElement webElement);
        //classname
        IWebElement GetElementByClassName(string className);
        IWebElement GetElementByClassName(string className, IWebElement webElement);
        IList<IWebElement> GetElementsByClassName(string className);
        IList<IWebElement> GetElementsByClassName(string className, IWebElement webElement);

    }

    public class SeleniumScraping : IBrowserScraping
    {
        private readonly IWebDriver _driver;
        public SeleniumScraping(IWebDriver driver)
        {
            _driver = driver;
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
            return _driver.FindElement(By.XPath(xPath));
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

        public IWebElement GetElementByClassName(string className)
        {
            return _driver.FindElement(By.ClassName(className));
        }

        public IList<IWebElement> GetElementsByClassName(string className)
        {
            return _driver.FindElements(By.ClassName(className));
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
