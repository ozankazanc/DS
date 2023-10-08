using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Models
{

    public class DataOption
    {
        public ElementOption MainElement { get; set; }
        public List<ElementOption> SubElements { get; set; }
    }

    public class ElementOption
    {
        public string ElementName { get; set; }
        public SearchOption SearchOption { get; set; }

    }

    public class SearchOption
    {
        public SearchType SearchType { get; set; }
        public string SearchValue { get; set; }
    }

    public enum SearchType
    {
        xPath = 1,
        TagName = 2,
        ClassName = 3
    }
}
