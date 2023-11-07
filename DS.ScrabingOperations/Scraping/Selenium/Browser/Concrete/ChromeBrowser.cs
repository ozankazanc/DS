using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DS.Scraping.Scraping.Selenium.Browser.Concrete
{
    public class ChromeBrowser : ABrowser
    {
        public ChromeBrowser()
        {
            RunBrowser();
        }
        public override void RunBrowser()
        {
            base._webDriver = new ChromeDriver((ChromeDriverService)SetDriverService(), (ChromeOptions)SetOptions());
        }
        protected override DriverService SetDriverService()
        {
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            return service;
        }
        protected override DriverOptions SetOptions()
        {
            var options = new ChromeOptions();
            return options;
        }
    }

    public class ChromeLocalBrowser : ABrowser
    {
        private string forLocalBrowserBatFilePath = "ExternalFiles\\Chrome\\chromePort9222.bat";
        private string localChromeTempPath = "C:\\temp\\chromedata\\chromedriver.exe";
        public ChromeLocalBrowser()
        {
            RunBrowser();
        }
        public override void RunBrowser()
        {
            RunLocalBrowser();
            SetChromeEnvironmentVariable();

            base._webDriver = new ChromeDriver((ChromeOptions)SetOptions());
        }
        protected override DriverService SetDriverService()
        {
            throw new NotImplementedException();
        }
        protected override DriverOptions SetOptions()
        {
            var options = new ChromeOptions();
            options.DebuggerAddress = "localhost:9222";
            return options;
        }
        private void RunLocalBrowser()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var batPath = Path.Combine(outPutDirectory, forLocalBrowserBatFilePath);
            var processInfo = new ProcessStartInfo(batPath);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            Process.Start(processInfo);
        }
        private void SetChromeEnvironmentVariable()
        {
            Environment.SetEnvironmentVariable("webdriver.chrome.driver", localChromeTempPath);
        }
    }
}
