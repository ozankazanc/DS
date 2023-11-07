using DS.Scraping.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Utils
{
    public static class Helper
    {
        public static string SetXpathSearchByClassName(string tagName, string className)
        {
            return $"//{tagName}[@class='{className}']";
        }

        public static string SetXpathSearchByCssSelector(string tagName, string cssSelector)
        {
            return ($"{tagName} {cssSelector}").Replace(" ", ".");
        }
        public static string XPathNumerator(int num, string xPath)
        {
            var addedNumXpath = xPath.Replace(Constants.ROWNUMBERINCREASEKEY, num.ToString());
            return addedNumXpath;
        }

    }
}
