namespace Common.Extensions;

public static class TupleExtension
{
	public static (long x, long y) Add(this (long x, long y) a, long x, long y) => (a.x + x, a.y + y);

	public static (long x, long y) Add(this (long x, long y) a, (long x, long y) b) => (a.x + b.x, a.y + b.y);
	public static (double x, double y) Add(this (double x, double y) a, (double x, double y) b) => (a.x + b.x, a.y + b.y);

	public static (int x, int y) Add(this (int x, int y) a, int x, int y) => (a.x + x, a.y + y);

	public static (int x, int y) Add(this (int x, int y) a, (int x, int y) b) => (a.x + b.x, a.y + b.y);

	public static (long x, long y) Subtract(this (long x, long y) a, (long x, long y) b) => (a.x - b.x, a.y - b.y);

	public static (long x, long y) Subtract(this (long x, long y) a, long x, long y) => (a.x - x, a.y - y);

	public static (int x, int y) Subtract(this (int x, int y) a, (int x, int y) b) => (a.x - b.x, a.y - b.y);

	public static (int x, int y) Subtract(this (int x, int y)a, int x, int y) => (a.x - x, a.y - y);

	public static (int x, int y, int z) Add(this (int x, int y, int z) a, int x, int y, int z) => (a.x + x, a.y + y, a.z + z);

	public static (int x, int y, int z) Add(this (int x, int y, int z) a, (int x, int y, int z) p) => (a.x + p.x, a.y + p.y, a.z + p.z);

	public static (long x, long y) ToLong(this (int x, int y) a) => (a.x, a.y);

	public static int MultiplyComponents(this (int x, int y) a) => a.x * a.y;

	public static (int x, int y) ToTuple(this Vector2 v) => ((int)v.X, (int)v.Y);

	public static Vector2 ToVector2(this (int x, int y) v) => new(v.x, v.y);

}