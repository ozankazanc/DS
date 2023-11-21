using DS.ScrabingOperations.Models;
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
        DataTable GetDataWithMultiplePage(DataInformation dataInformation, string url);
        DataTable GetDataWithIncrementalLoad(DataInformation dataInformation, string url);
        IWebElement GetElementBySearchOption(SearchOption elementInformation);
        IWebElement GetElementBySearchOption(SearchOption elementInformation, IWebElement element);
        IWebElement GetElementBySearchOption(SearchOption elementInformation, IWebElement element, int numerator);
        IList<IWebElement> GetElementsBySearchOption(SearchOption dataInformation, IWebElement mainElement);
        string GetElementText(SearchOption searchOption, IWebElement element, int numerator = 0);
        string GetNextPageUrl(MultiplePagesType url);
        void SetPrevPageUrl(MultiplePagesType url);
    }
}
