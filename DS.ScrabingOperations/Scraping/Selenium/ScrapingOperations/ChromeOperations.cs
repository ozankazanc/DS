using DS.Scraping.Models;
using DS.Scraping.Scraping.Selenium.Browser;
using DS.Scraping.Scraping.Selenium.Browser.Concrete;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.Scraping.Scraping.Selenium.ScrapingOperations
{
    public class ChromeOperations
    {

        public void Run(DataInformation dataInformation, string url)
        {
            var chromeOperations = new CommonScrapingOperations(new ChromeLocalBrowser());
            chromeOperations.GetData(dataInformation, url);

        }
        
    }
}
