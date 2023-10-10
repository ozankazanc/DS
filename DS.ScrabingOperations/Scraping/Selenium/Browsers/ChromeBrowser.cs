using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium.Browsers
{
    internal class ChromeBrowser : ABrowser<ChromeDriver, ChromeDriverService, ChromeOptions>, IBrowser
    {
        protected override ChromeDriverService SetDriverSevice()
        {
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            return service;
        }
        protected override ChromeOptions SetOptions()
        {
            var options = new ChromeOptions();
            //options.AddArgument("--window-position=-32000,-32000");
            //options.AddArgument("headless");
            // options.DebuggerAddress = "localhost:9222";
            return options;
        }
        protected override ChromeDriver SetWebDriver()
        {
            return new ChromeDriver(SetDriverSevice(), SetOptions());
        }

        public override ChromeDriver SetCustomWebDriver()
        {
            return RunLocalChromeBrowser();
        }

        private ChromeDriver RunLocalChromeBrowser()
        {
            var options = SetOptions();
            options.DebuggerAddress = "localhost:9222";
            
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var batPath = Path.Combine(outPutDirectory, "ExternalFiles\\Chrome\\chromePort9222.bat");
            var processInfo = new ProcessStartInfo(@"C:\Users\ozank\Desktop\chromePort9222.bat");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            Process.Start(processInfo);

            Environment.SetEnvironmentVariable("webdriver.chrome.driver", "E:\\chromedata\\chromedriver.exe");
           
            return new ChromeDriver(SetOptions());
        }
    }






}
