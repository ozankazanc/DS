using DS.ScrabingOperations.Scraping.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleUI.TestCompanies
{
    public class Sahibinden
    {
        public string Url = "https://www.sahibinden.com/kiralik-daire/bursa?query_text_mf=bursa+kiralık+daire";

        public void TestIslemleri()
        {
            var scraping = new SeleniumScraping(Url);
            try
            {
                var mainElementXpath = "/html/body/div[5]/div[4]/form/div[1]/div[3]/table/tbody";
                var subElementsClassName = "searchResultsItem";
               

                var columnInformations = new Dictionary<string, string>();
                columnInformations.Add("İlan Başlığı", "classifiedTitle");
                columnInformations.Add("m2 Brüt", "prdct-desc-cntnr-ttl");
                columnInformations.Add("Fiyat", "prc-box-dscntd");
                columnInformations.Add("Oda Sayısı", "prc-box-dscntd");
                columnInformations.Add("İlan Tarihi", "prc-box-dscntd");
                columnInformations.Add("İlçe Semt", "prc-box-dscntd");

                //*[@id="searchResultsTable"]/tbody/tr[%x%]/td[4]
              

                var data = scraping.GetData(mainElementXpath, subElementsClassName, columnInformations);
            }
            finally
            {
                scraping.StopWrobser();
            }

        }
}
