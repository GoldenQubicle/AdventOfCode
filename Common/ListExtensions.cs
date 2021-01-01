using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ListExtensions
    {
        public static List<T> AddTo<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static List<T> InsertAt<T>(this List<T> list, int idx, T item)
        {
            list.Insert(idx, item);
            return list;
        }
    }
}
