using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class IEnumerableExtensions
    {
        public static void ForEach<T> (this IEnumerable<T> collection, Action<T> action)
        {
            foreach(var item in collection )
            {
                action(item);
            }
        }
    }
}
