using System.Text.RegularExpressions;

namespace Common.Extensions;

public static class RegexExtensions
{
    public static int AsInt(this Match m, string group) => int.Parse(m.Groups[group].Value);
    public static long AsLong(this Match m, string group) => long.Parse(m.Groups[group].Value);
}