using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.Scraping.Models
{

    public class DataInformation
    {
        public ElementInformation MainElement { get; set; }
        public ElementInformation SubElements { get; set; }
        public PageUrl NextPageUrl { get; set; }
        public PageUrl PreviousPageUrl { get; set; }
        public List<ColumnInformation> ColumnInformations { get; set; }
        public int? MaxRow { get; set; }

    }

    public class ElementInformation : SearchOption
    {

    }
    public class ColumnInformation : SearchOption
    {
        public string ColumnName { get; set; }
    }
    public class PageUrl : SearchOption
    {
        public string URL { get; set; }
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
        ClassName = 3,
        AttributeName = 4
    }
}
