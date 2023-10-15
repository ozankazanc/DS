using DS.ScrabingOperations.Models;
using Newtonsoft.Json.Converters;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium.BrowserScraping
{
    internal interface ISeleniumBrowserScraping : ISeleniumScraping
    {
        DataTable GetData(DataInformation dataInformation);
        string GetNextPageUrl(PageUrl url);
        string GetPrevPageUrl(PageUrl url);
        IWebElement GetElementBySearchOption(SearchOption dataInformation);
        IList<IWebElement> GetElementsBySearchOption(SearchOption dataInformation, IWebElement mainElement);
        string XPathNumerator(int num, string xPath);
        string GetOneElementTextByXPath(string xPath);
        string FindElementText(IWebElement element, SearchOption searchOption, int numerator = 0);
        List<string> GetListOfElementsTextByPath(params string[] xPath);
        List<string> GetListOfElementsTextByPath(List<string> xPaths);
    }
}
