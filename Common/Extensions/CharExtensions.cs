namespace Common.Extensions;

public static class CharExtensions
{
	public static long ToLong(this char c) => int.Parse(new(c, 1));

	public static int ToInt(this char c) => int.Parse(new(c, 1));
}