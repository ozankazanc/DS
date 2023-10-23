using DS.ScrabingOperations.Scraping.Selenium.ScrapingOperations;
using DS.Scraping.Scraping;
using DS.Scraping.Scraping.Selenium;
using DS.Scraping.Scraping.Selenium.ScrapingOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DS.ConsoleUI.TestCompanies
{
    public class Trendyol
    {
        public string Url = "https://www.trendyol.com/sr?q=nevresim&qt=nevresim&st=nevresim&os=1&sst=PRICE_BY_DESC";

        public void TestIslemleri()
        {
            var scraping = new ChromeOperations();
            
           try
            {
                var mainXpath = "/html/body/div[1]/div[3]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[4]/div[1]/div";
                var repetitiveClassName = "p-card-wrppr";
                var columnNames = new string[3] { "Açıklama", "Marka", "Fiyat" };
                var columnValuesClassNames = new string[3] { "prc-box-dscntd", "prdct-desc-cntnr-ttl", "prdct-desc-cntnr-name" };

                var columnInformations = new Dictionary<string, string>();
                columnInformations.Add("Marka", "prdct-desc-cntnr-name");
                columnInformations.Add("Açıklama", "prdct-desc-cntnr-ttl");
                columnInformations.Add("Fiyat", "prc-box-dscntd");

               // var data = scraping.GetData(mainXpath, repetitiveClassName, columnInformations);
            }
            finally
            {
                //scraping.CloseBrowser();
            }

        }
    }
}
