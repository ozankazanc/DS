using DS.Scraping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Models
{
    public class SearchOption
    {
        public SearchType SearchType { get; set; }
        public string SearchValue { get; set; }
    }
    public enum SearchType
    {
        xPath = 1,
        TagName = 2,
        ClassName = 3,
        CssSelector = 4,
        AttributeName = 5
    }
}
