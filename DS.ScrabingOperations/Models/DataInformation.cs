using DS.ScrabingOperations.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DS.Scraping.Models
{

    //to do: columnInformation için alternatif bir searchOption daha gerekli.
    //to do: column için default değer geçilmeli.
    public class DataInformation
    {
        public ElementInformation MainElement { get; set; }
        public List<ElementInformation> SubElements { get; set; }
        public PaginationInformation PaginationElement { get; set; }
        public List<ColumnInformation> ColumnInformations { get; set; }
        public int? MaxRow { get; set; }
        public int? StartRowIndex { get; set; }

    }

    public class ElementInformation : SearchOption
    {

    }
    public class ColumnInformation : SearchOption
    {
        public string ColumnName { get; set; }
        public bool AllowNullValue { get; set; } = false;
    }
    public class PaginationInformation : SearchOption
    {
        public string NextPageURL { get; set; }
        public string PrevPageURL { get; set; }
        public string PagingKey { get; set; }
        public string FinisherOperator { get; set; }

        public string GetEditedPagingKey
        {
            get
            {
                int startOfIndexPagingKey = NextPageURL.IndexOf(PagingKey);
                string clearedBeforePagingKey = NextPageURL.Remove(0, startOfIndexPagingKey);
                int IndexOfFinisherOperator = FinisherOperator.IsNotNull() ? clearedBeforePagingKey.IndexOf(FinisherOperator) : (clearedBeforePagingKey.Length - 1);
                string clearedPagingKeysValue = NextPageURL.Substring(startOfIndexPagingKey, IndexOfFinisherOperator);
                return clearedPagingKeysValue;
            }
        }
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
        CssSelector = 4,
        AttributeName = 5
    }
}
