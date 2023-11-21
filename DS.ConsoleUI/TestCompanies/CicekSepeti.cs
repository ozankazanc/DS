using DS.ScrabingOperations.Models;
using DS.ScrabingOperations.Utils;
using DS.Scraping.Models;
using DS.Scraping.Scraping.Selenium.ScrapingOperations;
using DS.Scraping.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DS.ConsoleUI.TestCompanies
{
    internal class CicekSepeti
    {
        public string Url = "https://www.ciceksepeti.com/extra-kolye-bileklik";

        public void TestIslemleri()
        {
            var scraping = new ChromeOperations();

            try
            {
                var dataInformation = new DataInformation
                {
                    MainElement = new ElementInformation
                    {
                        SearchType = SearchType.CssSelector,
                        SearchValue = Helper.SetXpathSearchByCssSelector(HtmlTags.div, "products products--category js-ajax-category-products")
                    },
                    SubElements = new List<ElementInformation>()
                    {
                        //new ElementInformation
                        //{
                        //    SearchType = SearchType.CssSelector,
                        //    SearchValue = Helper.SetXpathSearchByCssSelector(HtmlTags.div, "products__item js-category-item-hover js-product-item-for-countdown js-product-item products__item--featured-xlg has-date-base-campaing")
                        //}
                         new ElementInformation
                        {
                            SearchType = SearchType.ClassName,
                            SearchValue = "products__item"
                        }
                    },
                    MaxRow = 150,
                    ColumnInformations = new List<ColumnInformation>
                    {
                        new ColumnInformation
                        {
                            ColumnName = "Ürün Adı",
                            SearchInformation = new List<SearchOption>
                            {
                                new SearchOption
                                {
                                    SearchType = SearchType.ClassName,
                                    SearchValue = "products__item-title"
                                }
                            }
                        },
                        new ColumnInformation
                        {
                            ColumnName = "Etkileşim",
                            SearchInformation = new List<SearchOption>
                            {
                                new SearchOption
                                {
                                    SearchType = SearchType.ClassName,
                                    SearchValue = "products-stars__review-count",
                                } 
                            },
                            AllowNullValue =true
                        },
                        new ColumnInformation
                        {
                            ColumnName = "Fiyat",
                            SearchInformation = new List<SearchOption>
                            {
                                new SearchOption
                                {
                                    SearchType =SearchType.CssSelector,
                                    SearchValue = Helper.SetXpathSearchByCssSelector(HtmlTags.div,$"price price--now")
                                },
                                new SearchOption
                                {
                                    SearchType = SearchType.CssSelector,
                                    SearchValue = Helper.SetXpathSearchByCssSelector(HtmlTags.span,$"price__integer-value")
                                } 
                            },
                             AllowNullValue =true
                        },
                        new ColumnInformation
                        {
                            ColumnName = "Küsürat",
                            SearchInformation = new List<SearchOption>
                            {
                                new SearchOption
                                {
                                    SearchType = SearchType.ClassName,
                                    SearchValue = "price__decimal-value-with-currency"
                                }
                            },
                            AllowNullValue =true
                        },
                        new LinkColumnInformation
                        {
                            SearchInformation = new List<SearchOption>
                            {
                                new SearchOption
                                {
                                    SearchType = SearchType.CssSelector,
                                    SearchValue = Helper.SetXpathSearchByCssSelector(HtmlTags.a,$"products__item-link js-products__item-link")
                                },
                                new SearchOption
                                {
                                    SearchType = SearchType.AttributeName,
                                    SearchValue = "href"
                                }
                            },
                        },
                    },
                    
                    
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
