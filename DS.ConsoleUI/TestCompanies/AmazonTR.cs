using DS.ScrabingOperations.Models;
using DS.ScrabingOperations.Utils;
using DS.Scraping.Models;
using DS.Scraping.Scraping.Selenium.ScrapingOperations;
using DS.Scraping.Utils;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DS.ConsoleUI.TestCompanies
{
    internal class AmazonTR
    {
        public string Url = "https://www.amazon.com.tr/s?k=iphone+15+pro+max&crid=11YFI3DLYK3WL&sprefix=%2Caps%2C115&ref=nb_sb_ss_sx-trend-t-ps-d-purchases-ten-ca_1_0";

        public void TestIslemleri()
        {
            var scraping = new ChromeOperations();

            try
            {

                var dataInformation = new DataInformation
                {
                    MainElement = new ElementInformation
                    {
                        SearchType = SearchType.xPath,
                        SearchValue = Helper.SetXpathSearchByClassName(HtmlTags.div, "s-main-slot s-result-list s-search-results sg-row")
                    },
                    SubElements = new List<ElementInformation>()
                    {
                        new ElementInformation
                        {
                            SearchType = SearchType.xPath,
                            SearchValue = Helper.SetXpathSearchByClassName(HtmlTags.div, "sg-col-4-of-24 sg-col-4-of-12 s-result-item s-asin sg-col-4-of-16 AdHolder sg-col s-widget-spacing-small sg-col-4-of-20")
                        },
                        new ElementInformation
                        {
                            SearchType = SearchType.xPath,
                            SearchValue = Helper.SetXpathSearchByClassName(HtmlTags.div, "sg-col-4-of-24 sg-col-4-of-12 s-result-item s-asin sg-col-4-of-16 sg-col s-widget-spacing-small sg-col-4-of-20")
                        }
                    },
                    PaginationElement = new MultiplePagesType
                    {
                        SearchType = SearchType.ClassName,
                        SearchValue = "s-pagination-strip",
                        PagingKey = "page=",
                        FinisherOperator = "&"
                    },
                    MaxRow = 1000,
                    ColumnInformations = new List<ColumnInformation>
                    {
                        new ColumnInformation
                        {
                            ColumnName = "Ürün Başlığı",
                            SearchType = SearchType.CssSelector,
                            SearchValue = Helper.SetXpathSearchByCssSelector(HtmlTags.span,"a-size-base-plus a-color-base a-text-normal")
                        },
                        new ColumnInformation
                        {
                            ColumnName = "Fiyat",
                            SearchType = SearchType.ClassName,
                            SearchValue = $"a-price-whole",
                            AllowNullValue = true
                        },
                        new ColumnInformation
                        {
                            ColumnName = "Fiyat Küsürat",
                            SearchType = SearchType.ClassName,
                            SearchValue = $"a-price-fraction",
                             AllowNullValue =true
                        },
                          new ColumnInformation
                        {
                            ColumnName = "Etkileşim",
                            SearchType = SearchType.CssSelector,
                            SearchValue = Helper.SetXpathSearchByCssSelector(HtmlTags.span, "a-size-base s-underline-text"),
                            AllowNullValue =true

                        },
                    }
                };

                scraping.RunWithMultiplePage(dataInformation, Url);
            }
            finally
            {
                //scraping.CloseBrowser();
            }

        }
    }
}
