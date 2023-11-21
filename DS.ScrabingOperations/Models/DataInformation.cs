using DS.ScrabingOperations.Models;
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
        public List<ColumnInformation> ColumnInformations { get; set; }
        public List<ColumnInformation> InnerPageInformations { get; set; }
        public PaginationInformation PaginationElement { get; set; }
        public int? MaxRow { get; set; }
        public int? StartRowIndex { get; set; }

    }
}
