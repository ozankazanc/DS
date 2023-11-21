using DS.ScrabingOperations.Models;
using DS.Scraping.Models;
using DS.Scraping.Scraping.Selenium;
using DS.Scraping.Scraping.Selenium.ScrapingOperations;
using DS.Scraping.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleUI.TestCompanies
{
    public class Sahibinden
    {
        public string Url = "https://www.sahibinden.com/kiralik-daire/bursa?query_text_mf=bursa+kiralik+daire";

        public void TestIslemleri()
        {
            var scraping = new ChromeOperations();
            
            //var scraping = new SeleniumChromeScraping();
            //scraping.GoToUrl(Url, true);


            try
            {
                var mainElementXpath = "//*[@id=\"searchResultsTable\"]/tbody";
                var subElementsClassName = "searchResults";
                var nextPageUrl = "//*[@id=\"searchResultsSearchForm\"]/div[1]/div[3]/div[3]/div[1]/ul/li[15]/a";

                #region test

                //*[@id="searchResultsSearchForm"]/div[1]/div[3]/div[3]/div[1]/ul/li[16]/a
                //*[@id="search"]/div[1]/div[1]/div/span[1]/div[1]/div[25]/div/div/span/a[4]
                //*[@id="search"]/div[1]/div[1]/div/span[1]/div[1]/div[25]/div/div/span/a[5]
                //*[@id="search"]/div[1]/div[1]/div/span[1]/div[1]/div[25]/div/div/span/a[5]

                //*[@id="searchResultsTable"]/tbody/tr[1]/td[2]/a[1]

                //var columnInformations = new Dictionary<string, string>();
                //columnInformations.Add("İlan Başlığı", "classifiedTitle");
                //columnInformations.Add("m2 Brüt", "prdct-desc-cntnr-ttl");
                //columnInformations.Add("Fiyat", "prc-box-dscntd");
                //columnInformations.Add("Oda Sayısı", "prc-box-dscntd");
                //columnInformations.Add("İlan Tarihi", "prc-box-dscntd");
                //columnInformations.Add("İlçe Semt", "prc-box-dscntd");

                //*[@id="searchResultsTable"]/tbody/tr[1]
                //*[@id="searchResultsTable"]/tbody/tr[2]

                //*[@id="searchResultsTable"]/tbody/tr[1]/td[2]/a[1]
                #endregion

                var dataInformation = new DataInformation
                {
                    MainElement = new ElementInformation { SearchType = SearchType.xPath, SearchValue = mainElementXpath },
                    SubElements = new List<ElementInformation> { new ElementInformation { SearchType = SearchType.TagName, SearchValue = HtmlTags.tr } },
                    PaginationElement = new PaginationInformation { SearchType = SearchType.ClassName, SearchValue = "pageNaviButtons" },
                    MaxRow = 40,
                    ColumnInformations = new List<ColumnInformation>
                    {
                        new ColumnInformation
                        {
                            ColumnName = "İlan Başlığı",
                            SearchType = SearchType.ClassName,
                            SearchValue = "classifiedTitle"
                        },
                        new ColumnInformation
                        {
                            ColumnName = "m2",
                            SearchType = SearchType.xPath,
                            SearchValue = $"//*[@id=\"searchResultsTable\"]/tbody/tr[{Constants.ROWNUMBERINCREASEKEY}]/td[3]"
                        },
                        new ColumnInformation
                        {
                            ColumnName = "Oda Sayısı",
                            SearchType = SearchType.xPath,
                            SearchValue = $"//*[@id=\"searchResultsTable\"]/tbody/tr[{Constants.ROWNUMBERINCREASEKEY}]/td[4]"
                        },
                         new ColumnInformation
                        {
                            ColumnName = "Fiyat",
                            SearchType = SearchType.xPath,
                            SearchValue = $"//*[@id=\"searchResultsTable\"]/tbody/tr[{Constants.ROWNUMBERINCREASEKEY}]/td[5]/div/span"
                        },
                          new ColumnInformation
                        {
                            ColumnName = "İlan Tarihi",
                            SearchType = SearchType.xPath,
                            SearchValue = $"//*[@id=\"searchResultsTable\"]/tbody/tr[{Constants.ROWNUMBERINCREASEKEY}]/td[6]/span[1]"
                        },
                            new ColumnInformation
                        {
                            ColumnName = "İlçe Semt",
                            SearchType = SearchType.xPath,
                            SearchValue = $"//*[@id=\"searchResultsTable\"]/tbody/tr[{Constants.ROWNUMBERINCREASEKEY}]/td[7]"
                        }
                    }

                };

                scraping.RunWithMultiplePage(dataInformation,Url);
            }
            finally
            {
                //scraping.CloseBrowser();
            }

        }
    }
}
