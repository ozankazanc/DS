using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
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
    internal class EdgeBrowser : ABrowser<EdgeDriver, EdgeDriverService, EdgeOptions>, IBrowser
    {
        protected override EdgeDriverService SetDriverSevice()
        {
            var service = EdgeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            return service;
        }

        protected override EdgeOptions SetOptions()
        {
            var options = new EdgeOptions();
            //options.AddArgument("--window-position=-32000,-32000");
            //options.AddArgument("headless");
            options.DebuggerAddress = "localhost:9222";
            return options;
        }

        protected override EdgeDriver SetWebDriver()
        {
            return new EdgeDriver(SetDriverSevice(), SetOptions());
        }
    }
}
