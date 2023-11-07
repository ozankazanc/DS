using DS.ScrabingOperations.Utils;
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
        private List<string> pageLinks;
        public CommonScrapingOperations(ABrowser testABrowser)
        {
            _browser = testABrowser;
        }

        public DataTable GetData(DataInformation dataInformation, string url)
        {
            _browser.GoToUrl(url);

            if (dataTable.IsNull())
            {
                dataTable = new DataTable();
                foreach (var columnInformation in dataInformation.ColumnInformations)
                    dataTable.Columns.Add(columnInformation.ColumnName, typeof(string));
            }

            var mainElement = GetElementBySearchOption(dataInformation.MainElement);
            var subElements = new List<IWebElement>();
            foreach (var subElement in dataInformation.SubElements)
                subElements.AddRange(GetElementsBySearchOption(subElement, mainElement).Cast<IWebElement>());

            var numerator = dataInformation.StartRowIndex.HasValue ? (dataInformation.StartRowIndex.Value - 1) : 0;
            foreach (var element in subElements)
            {
                var rowValues = new List<string>();
                numerator++;
                foreach (var columnInformation in dataInformation.ColumnInformations)
                {
                    var value = GetElementText(columnInformation, element, numerator);
                    //var attribute = GetElementAttributeValue(columnInformation, element, "class");
                    if (value.IsNotNull() || columnInformation.AllowNullValue)
                        rowValues.Add(value);
                }

                //kayıt arasina alakasiz bir row girerse ekleme
                if (rowValues.Count == dataTable.Columns.Count)
                    dataTable.Rows.Add(rowValues.ToArray());
            }

            //hasNextPage
            if (dataInformation.PaginationElement.IsNotNull())
                dataInformation.PaginationElement.NextPageURL = GetNextPageUrl(dataInformation.PaginationElement);

            if (!IsLastPageCheck(dataInformation.PaginationElement) && (dataInformation.MaxRow.HasValue && dataInformation.MaxRow.Value > dataTable.Rows.Count))
            {
                GetData(dataInformation, dataInformation.PaginationElement.NextPageURL);
            }

            _browser.CloseBrowser();

            return dataTable;
        }
        private bool IsLastPageCheck(PaginationInformation paginationElement)
        {
            var isLast = true;
            if (paginationElement.IsNotNull())
            {
                if (pageLinks.IsNull())
                {
                    pageLinks = new List<string>();
                    isLast = false;
                }
                else
                {
                    if (paginationElement.PagingKey.IsNull())
                        isLast = pageLinks.Any(recordedPageUrl => recordedPageUrl == paginationElement.NextPageURL);
                    else
                        isLast = pageLinks.Any(recordedEditedPagingKey => recordedEditedPagingKey == paginationElement.GetEditedPagingKey);
                }
            }

            if (!isLast)
            {
                if (paginationElement.PagingKey.IsNull())
                    pageLinks.Add(paginationElement.NextPageURL);
                else
                    pageLinks.Add(paginationElement.GetEditedPagingKey);
            }
            else
                pageLinks = null;

            return isLast;
        }
        public IWebElement GetElementBySearchOption(SearchOption elementInformation)
        {
            return GetElementBySearchOption(elementInformation, null);
        }
        public IWebElement GetElementBySearchOption(SearchOption elementInformation, IWebElement element)
        {
            return GetElementBySearchOption(elementInformation, null, 0);
        }
        public IWebElement GetElementBySearchOption(SearchOption elementInformation, IWebElement element, int numerator)
        {
            IWebElement mainElement;
            try
            {
                switch (elementInformation.SearchType)
                {
                    case SearchType.xPath:
                        mainElement = _browser.GetElementByXPath(Helper.XPathNumerator(numerator, elementInformation.SearchValue), element);
                        break;
                    case SearchType.TagName:
                        mainElement = _browser.GetElementByTagName(elementInformation.SearchValue, element);
                        break;
                    case SearchType.ClassName:
                        mainElement = _browser.GetElementByClassName(elementInformation.SearchValue, element);
                        break;
                    case SearchType.CssSelector:
                        mainElement = _browser.GetElementByCssSelector(elementInformation.SearchValue, element);
                        break;
                    default:
                        throw new NotImplementedException($"Arama tipi bulunamadı.");
                }
                return mainElement;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message +
                           $" | Hata ile karşılaşıldı." +
                           $" SearchType: {elementInformation.SearchType} |" +
                           $" SearchValue: {elementInformation.SearchValue}");
            }

            return null;
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
                case SearchType.CssSelector:
                    subElement = _browser.GetElementsByCssSelector(searchOption.SearchValue, mainElement);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return subElement;
        }
        public string GetElementText(SearchOption searchOption, IWebElement element, int numerator = 0)
        {
            var searchedElement = GetElementBySearchOption(searchOption, element, numerator);
            return searchedElement.IsNull() ? null : searchedElement.Text;
        }
        public string GetElementAttributeValue(SearchOption searchOption, IWebElement element, string attributeName, int numerator = 0)
        {
            var searchedElement = GetElementBySearchOption(searchOption, element, numerator);
            return searchedElement.IsNull() ? null : searchedElement.GetAttribute(attributeName);
        }
        public string GetNextPageUrl(PaginationInformation url)
        {
            var mainElement = GetElementBySearchOption(url);
            var subElements = GetElementsBySearchOption(
                new SearchOption
                {
                    SearchType = SearchType.TagName,
                    SearchValue = HtmlTags.a
                },
                mainElement);
            return subElements.Last().GetAttribute("href");
        }
        public void SetPrevPageUrl(PaginationInformation url)
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
