using System;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<KeyValuePair<TKey, TValue>> action)
        {
            foreach(var kvp in dic)
            {
                action(kvp);
            }
        }
    }
}
