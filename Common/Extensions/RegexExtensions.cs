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

    public static int AsInt(this Match m, string group) => int.Parse(m.Groups[group].Success ? m.Groups[group].Value : "0");
    public static long AsLong(this Match m, string group) => long.Parse(m.Groups[group].Success ? m.Groups[group].Value : "0");

	public static int TryGetGroup(this MatchCollection mc, string group)
	{
		foreach (Match match in mc)
		{
			if (match.Groups[group].Success)
				return match.AsInt(group);
		}

		return 0;
	}
}