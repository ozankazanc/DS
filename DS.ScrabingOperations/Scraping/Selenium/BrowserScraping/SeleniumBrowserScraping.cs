using DS.ScrabingOperations.Models;
using DS.ScrabingOperations.Scraping.Selenium.Browsers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var mainElement = ElementOperationBySearchType(dataInformation);
            var subElements = SubElementsOperationBySearchType(dataInformation, mainElement);

            foreach (var element in subElements)
            {
                var rowValues = new List<string>();
                var numerator = 0;
                foreach (var columnInformation in dataInformation.ColumnInformations)
                {
                    var value = string.Empty;
                    switch (columnInformation.SearchType)
                    {
                        case SearchType.xPath:
                            value = element.FindElement(By.XPath(XPathNumerator(ref numerator, columnInformation.SearchValue))).Text;
                            break;
                        case SearchType.TagName:
                            value = element.FindElement(By.TagName(columnInformation.SearchValue)).Text;
                            break;
                        case SearchType.ClassName:
                            value = element.FindElement(By.ClassName(columnInformation.SearchValue)).Text;
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                    rowValues.Add(value);
                }
                dataTable.Rows.Add(rowValues);
            }

            return dataTable;
        }

        public DataTable GetData(string mainElementXPath, string subElementsClassName, Dictionary<string, string> columnsWithClassNames)
        {
            var dataTable = new DataTable();

            foreach (var columnName in columnsWithClassNames.Keys)
            {
                dataTable.Columns.Add(columnName, typeof(string));
            }

            var mainElement = GetElementByXPath(mainElementXPath);
            var elements = GetElementsByClassName(subElementsClassName, mainElement);

            foreach (var element in elements)
            {
                var rowValues = new List<string>();
                foreach (var value in columnsWithClassNames.Values)
                    rowValues.Add(element.FindElement(By.ClassName(value)).Text);

                dataTable.Rows.Add(rowValues.ToArray());
            }

            return dataTable;
        }

        public IWebElement ElementOperationBySearchType(DataInformation dataInformation)
        {
            IWebElement mainElement;

            switch (dataInformation.MainElement.SearchType)
            {
                case SearchType.xPath:
                    mainElement = GetElementByXPath(dataInformation.MainElement.SearchValue);
                    break;
                case SearchType.TagName:
                    mainElement = GetElementByTagName(dataInformation.MainElement.SearchValue);
                    break;
                case SearchType.ClassName:
                    mainElement = GetElementByClassName(dataInformation.MainElement.SearchValue);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return mainElement;
        }

        public IList<IWebElement> SubElementsOperationBySearchType(DataInformation dataInformation, IWebElement mainElement)
        {
            IList<IWebElement> subElement;

            switch (dataInformation.SubElements.SearchType)
            {
                case SearchType.xPath:
                    subElement = GetElementsByXPath(dataInformation.SubElements.SearchValue, mainElement);
                    break;
                case SearchType.TagName:
                    subElement = GetElementsByTagName(dataInformation.SubElements.SearchValue, mainElement);
                    break;
                case SearchType.ClassName:
                    subElement = GetElementsByClassName(dataInformation.SubElements.SearchValue, mainElement);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return subElement;
        }

        public string XPathNumerator(ref int num, string xPath)
        {
            num++;
            var addedNumXpath = xPath.Replace("#x#", num.ToString());
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
