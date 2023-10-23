using DS.Scraping.Models;
using Newtonsoft.Json;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Scraping.Scraping.Selenium.ScrapingOperations
{
    public interface ICommonScrapingOperations
    {
        DataTable GetData(DataInformation dataInformation, string url);
        IWebElement GetElementBySearchOption(SearchOption dataInformation);
        IList<IWebElement> GetElementsBySearchOption(SearchOption dataInformation, IWebElement mainElement);
        string XPathNumerator(int num, string xPath);
        string FindElementText(IWebElement element, SearchOption searchOption, int numerator = 0);
        void SetNextPageUrl(PageUrl url);
        void SetPrevPageUrl(PageUrl url);
    }
}
