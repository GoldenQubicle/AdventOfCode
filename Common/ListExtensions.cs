using System.Collections.Generic;

namespace Common
{
    public static class ListExtensions
    {
        /// <summary>
        /// Add the item to the current list and return the result as a new List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">the list to be expanded</param>
        /// <param name="item">the item to be added</param>
        /// <returns>A new list</returns>
        public static List<T> Expand<T>(this List<T> list, T item)
        {
            var result = new List<T>( );
            result.AddRange(list);
            result.Add(item);
            return result;
        }

        public static List<T> InsertAt<T>(this List<T> list, int idx, T item)
        {
            list.Insert(idx, item);
            return list;
        }
    }
}
