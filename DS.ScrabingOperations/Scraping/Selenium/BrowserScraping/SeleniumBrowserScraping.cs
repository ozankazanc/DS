using DS.ScrabingOperations.Models;
using DS.ScrabingOperations.Scraping.Selenium.Browsers;
using DS.ScrabingOperations.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DS.ScrabingOperations.Scraping.Selenium.BrowserScraping
{
    internal class SeleniumBrowserScraping : ISeleniumBrowserScraping
    {
        private readonly IWebDriver _driver;
        public SeleniumBrowserScraping(IWebDriver driver)
        {
            _driver = driver;

        }

        public DataTable GetData(DataInformation dataInformation)
        {
            var dataTable = new DataTable();

            foreach (var columnInformation in dataInformation.ColumnInformations)
            {
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
                    rowValues.Add(value);
                }
                dataTable.Rows.Add(rowValues.ToArray());
            }

            return dataTable;
        }

        public IWebElement GetElementBySearchOption(SearchOption elementInformation)
        {
            IWebElement mainElement;

            switch (elementInformation.SearchType)
            {
                case SearchType.xPath:
                    mainElement = GetElementByXPath(elementInformation.SearchValue);
                    break;
                case SearchType.TagName:
                    mainElement = GetElementByTagName(elementInformation.SearchValue);
                    break;
                case SearchType.ClassName:
                    mainElement = GetElementByClassName(elementInformation.SearchValue);
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
                    subElement = GetElementsByXPath(searchOption.SearchValue, mainElement);
                    break;
                case SearchType.TagName:
                    subElement = GetElementsByTagName(searchOption.SearchValue, mainElement);
                    break;
                case SearchType.ClassName:
                    subElement = GetElementsByClassName(searchOption.SearchValue, mainElement);
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

        public string GetOneElementTextByXPath(string xPath)
        {
            var element = GetElementByXPath(xPath);
            return element.Text;
        }

        public List<string> GetListOfElementsTextByPath(params string[] xPath)
        {
            if (xPath.Length == 0)
                return null;

            var elements = new List<string>();

            foreach (var xp in xPath)
                elements.Add(GetOneElementTextByXPath(xp));

            return elements;
        }

        public List<string> GetListOfElementsTextByPath(List<string> xPaths)
        {
            if (xPaths.Count == 0)
                return null;
            else
                return GetListOfElementsTextByPath(xPaths.ToArray());
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

        public string GetNextPageUrl(PageUrl url)
        {
            throw new NotImplementedException();
        }

        public string GetPrevPageUrl(PageUrl url)
        {
            throw new NotImplementedException();
        }


        #region GetElementSide
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



        #endregion

    }
}
