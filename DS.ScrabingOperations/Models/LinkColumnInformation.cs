using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Models
{
    public class LinkColumnInformation : ColumnInformation
    {
        public LinkColumnInformation()
        {
            ColumnName = "Link";
            AllowNullValue = true;
        }
    }
}
