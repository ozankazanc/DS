using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Scraping.Selenium.Browsers
{
    public interface IBrowser<T>
    {
        T GetDriver(string url);
        T GetDriver(string url, int waitSecond);
        void WaitWhileReachingUrl(int milisecond);
        void CloseDriver();
    }
}