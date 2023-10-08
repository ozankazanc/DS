using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping
{
    public interface IScraping
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
}
