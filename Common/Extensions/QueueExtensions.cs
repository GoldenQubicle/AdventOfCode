using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class QueueExtensions
    {
        public static void EnqueueAll<T>(this Queue<T> q, IEnumerable<T> add) => add.ForEach(q.Enqueue);
    }
}
