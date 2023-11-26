using System.Text.RegularExpressions;

namespace Common.Extensions;

public static class RegexExtensions
{
	public static (int, int) AsIntTuple(this Match m, string group, string split)
	{
		var strings = m.Groups[group].Value.Split(split);
		return (int.Parse(strings[0]), int.Parse(strings[1]));
	}

    public static int AsInt(this Match m, string group) => int.Parse(m.Groups[group].Value);
    public static long AsLong(this Match m, string group) => long.Parse(m.Groups[group].Value);
}