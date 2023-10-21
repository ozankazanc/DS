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
        IWebElement GetElementBySearchOption(SearchOption dataInformation);
        IList<IWebElement> GetElementsBySearchOption(SearchOption dataInformation, IWebElement mainElement);
        string XPathNumerator(int num, string xPath);
        string FindElementText(IWebElement element, SearchOption searchOption, int numerator = 0);
        void SetNextPageUrl(PageUrl url);
        void SetPrevPageUrl(PageUrl url);

    }
}
