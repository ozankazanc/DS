using DS.ScrabingOperations.Models;
using DS.ScrabingOperations.Utils;
using DS.Scraping.Models;
using DS.Scraping.Scraping;
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
    public class Trendyol
    {
        public string Url = "https://www.trendyol.com/sr?q=Monitör&qt=Monitör&st=Monitör&os=1";

        public void TestIslemleri()
        {
            var scraping = new ChromeOperations();

            try
            {

                var dataInformation = new DataInformation
                {
                    MainElement = new ElementInformation
                    {
                        SearchType = SearchType.ClassName,
                        SearchValue = "prdct-cntnr-wrppr"
                    },
                    SubElements = new List<ElementInformation>()
                    {
                        new ElementInformation
                        {
                            SearchType = SearchType.xPath,
                            SearchValue = Helper.SetXpathSearchByClassName(HtmlTags.div, "p-card-wrppr with-campaign-view add-to-bs-card")
                        }
                    },
                    PaginationElement = new IncrementalLoadType
                    {
                      
                    },
                    MaxRow = 300,
                    ColumnInformations = new List<ColumnInformation>
                    {
                        new ColumnInformation
                        {
                            ColumnName = "Ürün Markası",
                            SearchType = SearchType.ClassName,
                            SearchValue = "prdct-desc-cntnr-ttl"
                        },
                        new ColumnInformation
                        {
                            ColumnName = "Ürün Detayı",
                            SearchType = SearchType.CssSelector,
                            SearchValue = Helper.SetXpathSearchByCssSelector(HtmlTags.span,"prdct-desc-cntnr-name hasRatings"),
                        },
                        new ColumnInformation
                        {
                            ColumnName = "Oylama Sayisi",
                            SearchType = SearchType.ClassName,
                            SearchValue = $"ratingCount",
                             AllowNullValue =true
                        },
                        new ColumnInformation
                        {
                            ColumnName = "Ürün Fiyatı",
                            SearchType = SearchType.ClassName,
                            SearchValue = "prc-box-dscntd",
                        },
                    }
                };

                scraping.RunWithIncrementalLoad(dataInformation, Url);
            }
            finally
            {
                //scraping.CloseBrowser();
            }

        }
    }
}
