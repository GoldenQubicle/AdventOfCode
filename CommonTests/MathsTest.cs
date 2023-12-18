using System.Collections.Generic;
using Common;
using NUnit.Framework;

namespace CommonTests
{
	internal class MathsTest
	{

		[TestCaseSource(nameof(GetShoeLaceTestCases))]
		public void CalculateShoeLaceArea((List<(long x, long y)> poly, long expected) test) =>
		 Assert.That(Maths.CalculateAreaShoeLace(test.poly), Is.EqualTo(test.expected));

		
		[TestCaseSource(nameof(GetCycleDetectionTestCases))]
		public void PatternShouldBeCorrectlyDetected((List<int> inputs, Maths.PatternDetectionResult<int> expected) test)
		{
			var result = Maths.DetectPattern(test.inputs);
			Assert.That(result.IsCycle, Is.EqualTo(test.expected.IsCycle));
			Assert.That(result.Pattern, Is.EqualTo(test.expected.Pattern));
			Assert.That(result.Idx, Is.EqualTo(test.expected.Idx));
		}

		
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



		public static IEnumerable<(List<(long x, long y)> poly, long expected)> GetShoeLaceTestCases()
		{
			yield return (new List<(long x, long y)> { (2, 7), (10, 1), (8, 6), (11, 7), (7, 10), (2, 7), }, 32);
			yield return (new List<(long x, long y)> { (3, 4), (5, 11), (12, 8), (9, 5), (5, 6), (3, 4), }, 30);
		}
		

		public static IEnumerable<(List<long> inputs, long expected)> GetLeastCommonMultipleTestCases()
		{
			yield return (new List<long> { 112, 15, 75 }, 8400);
			yield return (new List<long> { 35, 48, 3518, 157, 5 }, 463953840);
			yield return (new List<long> { 14893, 20513, 22199, 19951, 17141, 12083 }, 15995167053923);
		}


		public static IEnumerable<(List<int> inputs, Maths.PatternDetectionResult<int> expected)> GetCycleDetectionTestCases()
		{
			yield return (new List<int> { 2, 0, 6, 3, 1, 6, 3, 1, 6, 3, 1, 6, 3, 1 }, new Maths.PatternDetectionResult<int>(true, new( ) { 6, 3, 1 }, 2));
			yield return (new List<int> { 2342, 23440, 5434, 439392, 64345, 23834, 953, 4521, 6, 305, 2394572, 6, 837, 1 }, new Maths.PatternDetectionResult<int>( ));
			yield return (new List<int> { 7, 1, 6, 4, 5, 6, 8, 2, 9, 4, 6, 8, 2, 9, 4 }, new Maths.PatternDetectionResult<int>(true, new( ) { 6, 8, 2, 9, 4 }, 5));
		}
	}
}
