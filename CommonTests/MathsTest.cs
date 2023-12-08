using System.Collections.Generic;
using Common;
using NUnit.Framework;

namespace CommonTests
{
	internal class MathsTest
	{
		[TestCaseSource(nameof(GetLeastCommonMultipleTestCases))]
		public void LeastCommonMultipleShouldBeCorrectLong((List<long> input, long expected) test) =>
			Assert.That(Maths.LeastCommonMultiple(test.input), Is.EqualTo(test.expected));



		[TestCase(12, 36, 12)]
		[TestCase(348, 306, 6)]
		public void GreatestCommonDivisorShouldBeCorrectInt(int a, int b, int e) =>
			Assert.That(Maths.GreatestCommonDivisor(a, b), Is.EqualTo(e));


		[TestCase(345618f, 306684f, 126f)]
		[TestCase(6188f, 211684f, 68f)]
		public void GreatestCommonDivisorShouldBeCorrectFloat(float a, float b, float e) =>
			Assert.That(Maths.GreatestCommonDivisor(a, b), Is.EqualTo(e));


		public static IEnumerable<(List<long> inputs, long expected)> GetLeastCommonMultipleTestCases()
		{
			yield return (new List<long> { 112, 15, 75 }, 8400);
			yield return (new List<long> { 35, 48, 3518, 157, 5 }, 463953840);
			yield return (new List<long> { 14893, 20513, 22199, 19951, 17141, 12083 }, 15995167053923);
		}
	}
}
