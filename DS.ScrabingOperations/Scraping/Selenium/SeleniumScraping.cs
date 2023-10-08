using DS.ScrabingOperations.Models;
using DS.ScrabingOperations.Scraping.Selenium.Browsers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium
{
    public class SeleniumScraping
    {

        private IWebDriver _driver;
        private ChromeBrowser _chromeBrowser;
        private string _url;
        private const int PAGELOADINGSECOND = 1;

        public SeleniumScraping(string url)
        {
            _url = url;
            CreateChromeBrowser();
            CreateChromeDriver(url);
        }

        public string GetOneElementTextByXPath(string xPath)
        {
            var element = _chromeBrowser.GetElementByXPath(xPath);
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

        private void CreateChromeBrowser()
        {
            _chromeBrowser = new ChromeBrowser();
        }

        private void CreateChromeDriver(string url)
        {
            _driver = _chromeBrowser.GetDriver(url, PAGELOADINGSECOND);
        }

        public void GoToUrl(string url)
        {
            _url = url;
            CreateChromeDriver(url);

        }

        public void ScrollDown()
        {
            long scrollHeight = 0;

            do
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
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

        public void StopWrobser()
        {
            _chromeBrowser.CloseDriver();
        }

        public void RestartWrobser()
        {
            StopWrobser();
            _driver = _chromeBrowser.GetDriver(_url, 5);
        }

        public DataTable GetData(string mainElementXPath, string subElementsClassName, Dictionary<string, string> columnsWithClassNames)
        {
            var dataTable = new DataTable();

            foreach (var columnName in columnsWithClassNames.Keys)
            {
                dataTable.Columns.Add(columnName, typeof(string));
            }

            var mainElement = _chromeBrowser.GetElementByXPath(mainElementXPath);
            var elements = _chromeBrowser.GetElementsByClassName(subElementsClassName, mainElement);

            foreach (var element in elements)
            {
                var rowValues = new List<string>();
                foreach (var value in columnsWithClassNames.Values)
                    rowValues.Add(element.FindElement(By.ClassName(value)).Text);

                dataTable.Rows.Add(rowValues);
            }

            return dataTable;
        }

        public DataTable GetData(DataOption dataOption)
        {
            var dataTable = new DataTable();

            IWebElement mainElement = ElementOperationBySearchType(dataOption.MainElement);
            





        }

        private IWebElement ElementOperationBySearchType(ElementOption elementOption)
        {
            IWebElement mainElement; 

            switch (elementOption.SearchOption.SearchType)
            {
                case SearchType.xPath:
                    mainElement = _chromeBrowser.GetElementByXPath(elementOption.SearchOption.SearchValue);
                    break;
                case SearchType.TagName:
                    mainElement = _chromeBrowser.GetElementByTagName(elementOption.SearchOption.SearchValue);
                    break;
                case SearchType.ClassName:
                    mainElement = _chromeBrowser.GetElementByClassName(elementOption.SearchOption.SearchValue);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return mainElement;
        }
        private IList<IWebElement> ElementsOperationBySearchType(ElementOption elementOption)
        {
            IList<IWebElement> elements;

            switch (elementOption.SearchOption.SearchType)
            {
                case SearchType.xPath:
                    elements = _chromeBrowser.GetElementsByXPath(elementOption.SearchOption.SearchValue);
                    break;
                case SearchType.TagName:
                    elements = _chromeBrowser.GetElementsByTagName(elementOption.SearchOption.SearchValue);
                    break;
                case SearchType.ClassName:
                    elements = _chromeBrowser.GetElementsByClassName(elementOption.SearchOption.SearchValue);
                    break;
                default:
                    throw new NotImplementedException();
            }
            return elements;
        }

        private void XpathNumerator()
        {

        }








    }
}
