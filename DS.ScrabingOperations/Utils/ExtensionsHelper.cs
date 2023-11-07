using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DS.ScrabingOperations.Utils
{
    public static class ExtensionsHelper
    {
        public static bool IsNull(this object obj)
        {
            if (obj is string)
                return string.IsNullOrEmpty((string)obj);
            if (obj is DateTime)
                return (DateTime)obj > DateTime.MinValue;

            return (obj == null);
        }
        public static bool IsNotNull(this object obj)
        {
            return !IsNull(obj);
        }
        public static bool IsListContainsAnyElement<T>(this IList<T> list)
        {
            return IsNotNull(list) ? (list.Count > 0) : false;
        }
        public static bool IsListNotContainsAnyElement<T>(this IList<T> list)
        {
            return !IsListContainsAnyElement(list);
        }

    }
}
