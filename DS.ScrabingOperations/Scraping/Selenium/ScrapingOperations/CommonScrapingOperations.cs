using DS.Scraping.Models;
using DS.Scraping.Scraping;
using DS.Scraping.Scraping.Selenium.Browser;
using DS.Scraping.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.Scraping.Scraping.Selenium.ScrapingOperations
{
    public class CommonScrapingOperations : ICommonScrapingOperations
    {
        private ABrowser _browser;
        private DataTable dataTable;
        public CommonScrapingOperations(ABrowser testABrowser)
        {
            _browser = testABrowser;
        }

        public DataTable GetData(DataInformation dataInformation, string url)
        {
            _browser.GoToUrl(url);

            if (dataTable == null)
            {
                dataTable = new DataTable();
                foreach (var columnInformation in dataInformation.ColumnInformations)
                    dataTable.Columns.Add(columnInformation.ColumnName, typeof(string));
            }

            var mainElement = GetElementBySearchOption(dataInformation.MainElement);
            var subElements = GetElementsBySearchOption(dataInformation.SubElements, mainElement);

            var numerator = 0;
            foreach (var element in subElements)
            {
                var rowValues = new List<string>();
                numerator++;
                foreach (var columnInformation in dataInformation.ColumnInformations)
                {
                    var value = FindElementText(element, columnInformation, numerator);

                    if (!string.IsNullOrEmpty(value))
                        rowValues.Add(value);
                }

                //kayıt arasina alakasiz bir row girerse ekleme
                if (rowValues.Count == dataTable.Columns.Count)
                    dataTable.Rows.Add(rowValues.ToArray());

            }

            if (dataInformation.NextPageUrl != null && (dataInformation.MaxRow.HasValue && dataInformation.MaxRow.Value > dataTable.Rows.Count))
            {
                SetNextPageUrl(dataInformation.NextPageUrl);
                GetData(dataInformation, dataInformation.NextPageUrl.URL);
            }

            _browser.CloseBrowser();

            return dataTable;
        }
        public IWebElement GetElementBySearchOption(SearchOption elementInformation)
        {
            IWebElement mainElement;

            switch (elementInformation.SearchType)
            {
                case SearchType.xPath:
                    mainElement = _browser.GetElementByXPath(elementInformation.SearchValue);
                    break;
                case SearchType.TagName:
                    mainElement = _browser.GetElementByTagName(elementInformation.SearchValue);
                    break;
                case SearchType.ClassName:
                    mainElement = _browser.GetElementByClassName(elementInformation.SearchValue);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return mainElement;
        }
        public IList<IWebElement> GetElementsBySearchOption(SearchOption searchOption, IWebElement mainElement)
        {
            IList<IWebElement> subElement;

            switch (searchOption.SearchType)
            {
                case SearchType.xPath:
                    subElement = _browser.GetElementsByXPath(searchOption.SearchValue, mainElement);
                    break;
                case SearchType.TagName:
                    subElement = _browser.GetElementsByTagName(searchOption.SearchValue, mainElement);
                    break;
                case SearchType.ClassName:
                    subElement = _browser.GetElementsByClassName(searchOption.SearchValue, mainElement);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return subElement;
        }
        public string XPathNumerator(int num, string xPath)
        {
            var addedNumXpath = xPath.Replace(Constants.ROWNUMBERINCREASEKEY, num.ToString());
            return addedNumXpath;
        }
        public string FindElementText(IWebElement element, SearchOption searchOption, int numerator = 0)
        {
            string value = string.Empty;
            try
            {
                switch (searchOption.SearchType)
                {
                    case SearchType.xPath:
                        value = element.FindElement(By.XPath(XPathNumerator(numerator, searchOption.SearchValue))).Text;
                        break;
                    case SearchType.TagName:
                        value = element.FindElement(By.TagName(searchOption.SearchValue)).Text;
                        break;
                    case SearchType.ClassName:
                        value = element.FindElement(By.ClassName(searchOption.SearchValue)).Text;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return value;
        }
        public void SetNextPageUrl(PageUrl url)
        {
            var mainElement = GetElementBySearchOption(url);
            var subElements = GetElementsBySearchOption(
                new SearchOption
                {
                    SearchType = SearchType.TagName,
                    SearchValue = HtmlTags.a
                },
                mainElement);
            var nextUrl = subElements.Last().GetAttribute("href");
            url.URL = nextUrl;
        }
        public void SetPrevPageUrl(PageUrl url)
        {
            throw new NotImplementedException();
        }
        public void ScrollDown()
        {
            long scrollHeight = 0;

            do
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_browser;
                var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

                if (newScrollHeight == scrollHeight)
                {
                    break;
                }
                else
                {
                    scrollHeight = newScrollHeight;
                    Thread.Sleep(400);
                }
            } while (true);
        }
    }
}
