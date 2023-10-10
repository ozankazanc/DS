using DS.ScrabingOperations.Scraping;
using DS.ScrabingOperations.Scraping.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace DS.ConsoleUI.TestCompanies
{
    public class Trendyol
    {
        public string Url ="https://www.sahibinden.com/kiralik/bursa?query_text_mf=bursa+kiralik+ev&query_text=kiralik+ev";

        public void TestIslemleri()
        {
            var scraping = new SeleniumScraping(Url);
            try
            {
                var mainXpath = "/html/body/div[1]/div[3]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[3]/div[1]/div";
                var repetitiveClassName = "p-card-wrppr";
                var columnNames = new string[3] { "Marka", "Açıklama", "Fiyat" };
                var columnValuesClassNames = new string[3] { "prc-box-dscntd", "prdct-desc-cntnr-ttl", "prdct-desc-cntnr-name" };

                var columnInformations = new Dictionary<string, string>();
                columnInformations.Add("Marka", "prdct-desc-cntnr-name");
                columnInformations.Add("Açıklama", "prdct-desc-cntnr-ttl");
                columnInformations.Add("Fiyat", "prc-box-dscntd");

                var data = scraping.GetData(mainXpath, repetitiveClassName, columnInformations);
            }
            finally
            {
                scraping.StopWrobser();
            }

        }
    }
}
