using System.Collections.Generic;

namespace Common
{
    public static class ListExtensions
    {
        /// <summary>
        /// Add the item to the current list and return the result as a new List&lt;<typeparamref name="T"/>&gt;
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

        /// <summary>
        /// Replace an items at the specified index and return the result as a new List&lt;<typeparamref name="T"/>&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="idx"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<T> ReplaceAt<T>(this List<T> list, int idx, T item)
        {
            var result = new List<T>( );
            list.RemoveAt(idx);
            list.Insert(idx, item);
            result.AddRange(list);
            return result;
        }

        /// <summary>
        /// Insert an item at the specified index and return the result as a new List&lt;<typeparamref name="T"/>&gt;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="idx"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<T> InsertAt<T>(this List<T> list, int idx, T item)
        {
            var result = new List<T>( );
            list.Insert(idx, item);
            result.AddRange(list);
            return result;
        }
    }
}
