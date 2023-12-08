using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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


}