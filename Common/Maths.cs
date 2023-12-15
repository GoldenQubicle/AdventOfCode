using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using Common.Extensions;

namespace Common;

public static class Maths // dumb name but prevents namespace conflict with System.Math
{
	/// <summary>
	/// Calculate the Least Common Multiple for the given list of input values. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="inputs"></param>
	/// <returns></returns>
	public static T LeastCommonMultiple<T>(IEnumerable<T> inputs) where T : INumber<T> =>
		inputs.Aggregate(LeastCommonMultiple);



	/// <summary>
	/// Calculate the Least Common Multiple for the 2 given values. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T> =>
		T.Abs(a * b) / GreatestCommonDivisor(a, b);



	/// <summary>
	/// Calculate the Greatest Common Divisor (aka Greatest Common Factor) for the 2 given values. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T> =>
		b == T.Zero ? a : GreatestCommonDivisor(b, a % b);



	//shamelessly copied from https://stackoverflow.com/a/67403631
	/// <summary>
	/// Simple test to see a point is inside the given polygon. 
	/// </summary>
	/// <remarks>Note the polygon must be ordered correctly, and a new GraphicsPath object is created with every call!</remarks>
	/// <param name="polygon"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	public static bool IsPointInsidePolygon(IEnumerable<(int x, int y)> polygon, (int x, int y) p)
	{
		var path = new GraphicsPath( );
		path.AddPolygon(polygon.Select(p => new Point(p.x, p.y)).ToArray());

		var region = new Region(path);

		return region.IsVisible(p.x, p.y);
	}

	/// <summary>
	/// Calculate the Manhattan Distance between point a and b. 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="a"></param>
	/// <param name="b"></param>
	/// <returns></returns>
	public static T GetManhattanDistance<T>((T x, T y) a, (T x, T y) b) where T : INumber<T> =>
		T.Abs(a.x - b.x) + T.Abs(a.y - b.y);


	public static string HashToHexadecimal(string input)
	{
		using var md5 = MD5.Create( );
		var bytes = Encoding.UTF8.GetBytes(input);
		var hashBytes = md5.ComputeHash(bytes);
		var sb = new StringBuilder( );
		hashBytes.ForEach(b => sb.Append(b.ToString("X2")));
		return sb.ToString( );
	}
}