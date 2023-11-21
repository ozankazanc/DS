using DS.ScrabingOperations.Models;
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
using System.Xml.Linq;

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

        public DataTable GetDataWithMultiplePage(DataInformation dataInformation, string url)
        {
            _browser.GoToUrl(url);

            FillDataTableColumns(dataInformation.ColumnInformations);
            var mainElement = GetMainElement(dataInformation.MainElement);
            var subElements = GetSubElements(mainElement, dataInformation.SubElements);
            FillDataTableValues(dataInformation, subElements, ref dataTable);

            //hasNextPage
            MultiplePagesType multiplePagesType = null;
            if (dataInformation.PaginationElement.IsNotNull())
            {
                multiplePagesType = (MultiplePagesType)dataInformation.PaginationElement;
                multiplePagesType.NextPageURL = GetNextPageUrl(multiplePagesType);
                dataInformation.PaginationElement = multiplePagesType;
            }

            if (!IsLastPageCheck(multiplePagesType) && (dataInformation.MaxRow.HasValue && dataInformation.MaxRow.Value > dataTable.Rows.Count))
            {
                GetDataWithMultiplePage(dataInformation, multiplePagesType.NextPageURL);
            }

            AddLineNumberColumnInDataTable(ref dataTable);

            _browser.CloseBrowser();

            return dataTable;
        }
        public DataTable GetDataWithIncrementalLoad(DataInformation dataInformation, string url)
        {
            _browser.GoToUrl(url);

            FillDataTableColumns(dataInformation.ColumnInformations);
            var mainElement = GetMainElement(dataInformation.MainElement);
            var subElements = GetSubElements(mainElement, dataInformation.SubElements);

            ScrollToBannerSideHeight(mainElement);

            int elementCount = subElements.Count;
            while (elementCount < dataInformation.MaxRow)
            {
                if (elementCount == subElements.Count)
                    _browser.ScrollDown(GetScrollDownHeight(mainElement, true));
                else
                    _browser.ScrollDown(GetScrollDownHeight(mainElement, false));

                elementCount += subElements.Count;
            }

            mainElement = GetMainElement(dataInformation.MainElement);
            subElements = GetSubElements(mainElement, dataInformation.SubElements);

            FillDataTableValues(dataInformation, subElements, ref dataTable);
            AddLineNumberColumnInDataTable(ref dataTable);
            
            _browser.CloseBrowser();

            return dataTable;
        }

        public void GetInnerPageData(DataInformation dataInformation)
        {
            FillDataTableInnerPageColumns(dataInformation.InnerPageInformations);
            var startColumnIndex = dataInformation.ColumnInformations.Count - 1;

            var dtInnerPage = new DataTable();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var startIndex = startColumnIndex;
                var link = dataTable.Rows[i]["Link"].IsNotNull() ? Convert.ToString(dataTable.Rows[i]["Link"]) : null;

                if (link.IsNotNull())
                {
                    _browser.GoToUrl(link);

                }
            }

        }

        public void FillDataTableColumns(List<ColumnInformation> columnInformations)
        {
            if (dataTable.IsNull())
            {
                dataTable = new DataTable();
                dataTable.Columns.Add("Line", typeof(string));
                foreach (var columnInformation in columnInformations)
                    dataTable.Columns.Add(columnInformation.ColumnName, typeof(string));
            }
        }
        public void FillDataTableInnerPageColumns(List<ColumnInformation> columnInformations)
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("Line", typeof(string));
            foreach (var columnInformation in columnInformations)
                dataTable.Columns.Add(columnInformation.ColumnName, typeof(string));
        }
        public void FillDataTableValues(DataInformation dataInformation, IList<IWebElement> subElements, ref DataTable dataTable)
        {
            var numerator = dataInformation.StartRowIndex.HasValue ? (dataInformation.StartRowIndex.Value - 1) : 0;
            foreach (var element in subElements)
            {
                var rowValues = new List<string>() { "0" }; //"0" -> line için eklendi
                numerator++;
                foreach (var columnInformation in dataInformation.ColumnInformations)
                {
                    var elementValue = element;
                    foreach (var searchInformation in columnInformation.SearchInformation)
                    {
                        elementValue = GetElementBySearchOption(searchInformation, elementValue, numerator);
                    }

                    string value = string.Empty;
                    var lastSearchInformation = columnInformation.SearchInformation.LastOrDefault();

                    if (lastSearchInformation.SearchType == SearchType.AttributeName)
                        value = GetElementAttributeValue(elementValue, lastSearchInformation.SearchValue);
                    else
                        value = GetElementText(elementValue);

                    if (value.IsNotNull() || columnInformation.AllowNullValue)
                        rowValues.Add(value);
                }

                //kayıt arasina alakasiz bir row girerse ekleme
                if (rowValues.Count == (dataTable.Columns.Count))
                    dataTable.Rows.Add(rowValues.ToArray());
            }
        }
        public IWebElement GetMainElement(ElementInformation mainElementInformation)
        {
            return GetElementBySearchOption(mainElementInformation);
        }
        public IList<IWebElement> GetSubElements(IWebElement mainElement, List<ElementInformation> subElementInformations)
        {
            var subElements = new List<IWebElement>();
            foreach (var subElementInformation in subElementInformations)
                subElements.AddRange(GetElementsBySearchOption(subElementInformation, mainElement).Cast<IWebElement>());

            return subElements;
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
                    case SearchType.AttributeName:
                        mainElement = element; //attribute için arama yapmiyoruz.
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
            return GetElementText(searchedElement);
        }
        public string GetElementText(IWebElement element)
        {
            return element.IsNull() ? null : element.Text;
        }
        public string GetElementAttributeValue(SearchOption searchOption, IWebElement element, string attributeName, int numerator = 0)
        {
            var searchedElement = GetElementBySearchOption(searchOption, element, numerator);
            return GetElementAttributeValue(searchedElement, attributeName);
        }
        public string GetElementAttributeValue(IWebElement element, string attributeName)
        {
            return element.IsNull() ? null : element.GetAttribute(attributeName);
        }
        private bool IsLastPageCheck(MultiplePagesType paginationElement)
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
            else
                return isLast;

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
        public string GetNextPageUrl(MultiplePagesType url)
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
        public void SetPrevPageUrl(MultiplePagesType url)
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

                // var newScrollHeight = (long)js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight); return document.body.scrollHeight;");

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
        public void ScrollToBannerSideHeight(IWebElement mainElement)
        {
            _browser.ScrollDown(mainElement.Location.Y);
            _browser.WaitWhileReachingUrl(2000);
        }
        public int GetScrollDownHeight(IWebElement mainElement, bool firstScroll)
        {
            if (firstScroll)
                return mainElement.Size.Height - (mainElement.Size.Height / 4);
            else
                return mainElement.Size.Height;
        }
        public void AddLineNumberColumnInDataTable(ref DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
                dataTable.Rows[i]["Line"] = (i + 1).ToString();
        }
    }
}
