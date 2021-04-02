using System;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (dic.ContainsKey(key))
                dic[key] = value;
            else 
                dic.Add(key, value);
        }

        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<KeyValuePair<TKey, TValue>> action)
        {
            foreach(var kvp in dic)
            {
                action(kvp);
            }
        }
    }
}
