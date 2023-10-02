using DS.ScrabingOperations.Scraping.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string xPath = "/html/body/div[2]/div[2]/div[4]/div/div/div/ul/li[2]/div/div[1]/ul";
            string url = "https://www.vestel.com.tr";

            SeleniumScraping scrapping = new SeleniumScraping("url");

            scrapping.get

        }
    }
}
