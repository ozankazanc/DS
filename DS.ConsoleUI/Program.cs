using DS.ConsoleUI.TestCompanies;
using DS.Scraping.Scraping.Selenium;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DS.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {







            CicekSepeti trendyol = new CicekSepeti();
            trendyol.TestIslemleri();


            //Trendyol trendyol = new Trendyol();
            //trendyol.TestIslemleri();


            //Sahibinden sahibinden = new Sahibinden();
            //sahibinden.TestIslemleri();

            //AmazonTR amazonTR = new AmazonTR();
            //amazonTR.TestIslemleri();




            // System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", "/path/to/chromedriver");








            //string clickxpath = "/html/body/div[8]/div[3]/div[2]/a";
            //string xpath = "/html/body/div[8]/div[2]/div/div[2]/div/div/div[1]/div/table/tbody/tr[1]/td[3]";
            //string url = "https://borsaistanbul.com/tr/";
            //string trendyol = "https://www.trendyol.com/sr?q=xbox&qt=xbox&st=xbox&os=1&sst=PRICE_BY_DESC";

            //SeleniumScraping scrapping = new SeleniumScraping(trendyol);

            //scrapping.ScrollDown();



            //var getTableXPath = xpath.Substring(0, xpath.LastIndexOf("table") + 5);

            //var element = scrapping.GetElementByXPath(clickxpath);
            //element.Click();

            //var elementthead = element.FindElement(By.TagName("thead"));
            //var elementsthead = elementthead.FindElements(By.TagName("th"));





            //foreach (var ele in elementsthead)
            //{
            //    var hasThead = element.TagName == "thead";
            //    var hasTBody = element.TagName == "tbody";
            //}

            //var res = scrapping.GetElementByXPath(xpathImkb);

            // var res = scrapping.GetListOfElementsTextByPath(xPath, xPath2);

            //var res = scrapping.GetElementByXPath(xPath);

            //IList<IWebElement> tableRow = res.FindElements(By.TagName("li"));
            //var element = tableRow[0].FindElement(By.TagName("a")).FindElement(By.TagName("span"));
            //var t = element.Text;
            //var k = element.GetAttribute("href");



        }


    }
}
