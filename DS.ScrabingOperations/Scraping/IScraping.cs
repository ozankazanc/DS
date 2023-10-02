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
        IWebElement GetElementByXPath(string xPath);
        IWebElement GetElementByXPath(string xPath, IWebElement webElement);
        IList<IWebElement> GetElementsByXPath(string xPath);
        IList<IWebElement> GetElementsByXPath(string xPath, IWebElement webElement);

        IWebElement GetElementByTagName(string tagName);
        IWebElement GetElementByTagName(string tagName, IWebElement webElement);
        IList<IWebElement> GetElementsByTagName(string tagName);
        IList<IWebElement> GetElementsByTagName(string tagName, IWebElement webElement);

    }
}
