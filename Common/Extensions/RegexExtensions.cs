using System.Linq;
using System.Text.RegularExpressions;

namespace Common.Extensions;

public static class RegexExtensions
{
	public static (int, int) AsIntTuple(this Match m, string group, string split)
	{
		var strings = m.Groups[group].Value.Split(split);
		return (int.Parse(strings[0]), int.Parse(strings[1]));
	}

    public static int AsInt(this Match m, string group) => int.Parse(m.Groups[group].Success ? m.Groups[group].Value.Where(char.IsDigit).AsString() : "0");

    public static long AsLong(this Match m, string group) => long.Parse(m.Groups[group].Success ? m.Groups[group].Value.Where(char.IsDigit).AsString( ) : "0");

    public static string AsString(this Match m, string group) => m.Groups[group].Success ? m.Groups[group].Value : string.Empty;

	public static int GetGroupAsInt(this MatchCollection mc, string group)
	{
		foreach (Match match in mc)
		{
			if (match.Groups[group].Success)
				return int.Parse(match.Groups[group].Value);
		}

		return 0;
	}

	public static string GetGroup(this MatchCollection mc, string group)
	{
		foreach (Match match in mc)
		{
			if (match.Groups[group].Success)
				return match.Groups[group].Value;
		}

		return string.Empty;
	}

}