using DS.ScrabingOperations.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Models
{
    public class PaginationInformation : SearchOption
    {

    }
    public class MultiplePagesType : PaginationInformation
    {
        public string NextPageURL { get; set; }
        public string PrevPageURL { get; set; }
        public string PagingKey { get; set; }
        public string FinisherOperator { get; set; }

        /// <summary>
        /// www.sahibinden.com/emlak-konut/bursa?pagingOffset=20&query_text_mf=bursa+ev&query_text=ev
        /// PagingKey=> "pagingOffset="
        /// FinisherOperator=> "&"
        /// </summary>
        public string GetEditedPagingKey
        {
            get
            {   //www.sahibinden.com/emlak-konut/bursa?pagingOffset=20&query_text_mf=bursa+ev&query_text=ev
                int startOfIndexPagingKey = NextPageURL.IndexOf(PagingKey);
                //pagingOffset=20&query_text_mf=bursa+ev&query_text=ev
                string clearedBeforePagingKey = NextPageURL.Remove(0, startOfIndexPagingKey);
                int IndexOfFinisherOperator = FinisherOperator.IsNotNull() ? clearedBeforePagingKey.IndexOf(FinisherOperator) : (clearedBeforePagingKey.Length - 1);
                //pagingOffset=20
                string clearedPagingKeysValue = NextPageURL.Substring(startOfIndexPagingKey, IndexOfFinisherOperator);
                return clearedPagingKeysValue;
            }
        }
    }
    public class IncrementalLoadType : PaginationInformation
    {
        public int HorizantalElementCount { get; set; }
        public int VerticalElementCount { get; set; }

       
    }

}
