namespace Common.Extensions;

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
		foreach (var kvp in dic)
		{
			action(kvp);
		}
	}

	public static Dictionary<TKey, TValue> AddNew<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
	{
		dic.Add(key, value);
		return dic;
	}

	public static Dictionary<TKey, TValue> AddTo<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, Func<TValue, TValue> func)
	{
		if(!dic.ContainsKey(key))
			dic.Add(key, default(TValue));
		dic[key] = func(dic[key]);
		return dic;
	}
}