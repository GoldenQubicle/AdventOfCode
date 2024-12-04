namespace Common.Extensions;

public static class StringExtensions
{
	public static string AsString(this IEnumerable<char> c) => new (c.ToArray());

	public static List<string> ToList(this string s) => new() { s };

	public static int ToInt(this string s) => int.Parse(s.Where(char.IsDigit).AsString());

	public static long ToLong(this string s) => long.Parse(s.Where(char.IsDigit).AsString( ));

	public static bool HasInteger(this string s) => s.Any(char.IsDigit);

	public static string ReplaceAt(this string s, int idx, string n, int r)
	{
		s = s.Remove(idx, r);
		s = s.Insert(idx, n);
		return s;
	}

	public static string ReplaceAt(this string s, int idx, string n)
	{
		s = s.Remove(idx, n.Length);
		s = s.Insert(idx, n);
		return s;
	}

	public static string ReplaceAt(this string s, int idx, char c)
	{
		s = s.Remove(idx, 1);
		s = s.Insert(idx, c.ToString());
		return s;
	}

	public static long ToDecimal(this StringBuilder sb) => Convert.ToInt64(sb.ToString(), 2);
}