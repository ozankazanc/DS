using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium.Browsers
{
    internal class FirefoxBrowser : ABrowser<FirefoxDriver, FirefoxDriverService, FirefoxOptions>, IBrowser
    {
        protected override FirefoxDriverService SetDriverSevice()
        {
            var service = FirefoxDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            return service;
        }

        protected override FirefoxOptions SetOptions()
        {
            var options = new FirefoxOptions();
            //options.AddArgument("--window-position=-32000,-32000");
            //options.AddArgument("headless");
            //options.DebuggerAddress = "localhost:9222";
            return options;
        }

        protected override FirefoxDriver SetWebDriver()
        {
            return new FirefoxDriver(SetDriverSevice(), SetOptions());
        }
    }
}
