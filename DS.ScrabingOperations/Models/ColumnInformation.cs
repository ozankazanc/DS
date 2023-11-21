using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Models
{
    public class ColumnInformation 
    {
        public string ColumnName { get; set; }
        public bool AllowNullValue { get; set; } = false;
        public List<SearchOption> SearchInformation { get; set; }
    }
}
