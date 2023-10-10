using DS.ScrabingOperations.Models;
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
        DataTable GetData(string mainElementXPath, string subElementsClassName, Dictionary<string, string> columnsWithClassNames);
        IWebElement ElementOperationBySearchType(DataInformation dataInformation);
        IList<IWebElement> SubElementsOperationBySearchType(DataInformation dataInformation, IWebElement mainElement);
        string XPathNumerator(ref int num, string xPath);
        string GetOneElementTextByXPath(string xPath);
        List<string> GetListOfElementsTextByPath(params string[] xPath);
        List<string> GetListOfElementsTextByPath(List<string> xPaths);
    }
}
