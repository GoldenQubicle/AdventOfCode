using AoC2023;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2023Tests
{
	public class Day05Test
	{
		Day05 day05;

		[SetUp]
		public void Setup()
		{
			day05 = new Day05("day05test1");
		}

		[Test]
		public void Part1()
		{
			var actual = day05.SolvePart1( );
			Assert.That(actual, Is.EqualTo("35"));
		}

		[Test]
		public void Part2()
		{
			var actual = day05.SolvePart2( );
			Assert.That(actual, Is.EqualTo("46"));
		}

		[TestCaseSource(nameof(GetRangeTestCases))]
		public void GetRange(GetRangeTestCase testCase)
		{
			var actual = Day05.Mappings.GetRange(testCase.source, testCase.range, testCase.offset);
			Assert.That(actual, Is.EqualTo(testCase.e));
		}


		public static IEnumerable<GetRangeTestCase> GetRangeTestCases()
		{
			yield return new GetRangeTestCase((25, 40), (30, 50), 0L, new List<(int, int)> { (30, 40), (41, 50) });
			yield return new GetRangeTestCase((25, 40), (30, 35), 0L, new List<(int, int)> { (30, 35) });
			yield return new GetRangeTestCase((25, 40), (10, 30), 0L, new List<(int, int)> { (10, 24), (25, 30) });
			yield return new GetRangeTestCase((25, 40), (10, 50), 0L, new List<(int, int)> { (10, 24), (25, 40), (41, 50) });
			yield return new GetRangeTestCase((2, 2), (2, 2), 0L, new List<(int, int)> { (2,2) });
			yield return new GetRangeTestCase((2, 3), (2, 2), 4L, new List<(int, int)> { (6,6) });
			yield return new GetRangeTestCase((2, 2), (2, 3), 4L, new List<(int, int)> { (6,6), (3,3) });
			yield return new GetRangeTestCase((2, 2), (2, 3), -2L, new List<(int, int)> { (0,0), (3,3) });
			yield return new GetRangeTestCase((2, 13), (12, 13), -2L, new List<(int, int)> { (10,11) });
			yield return new GetRangeTestCase((2, 13), (13, 13), 20L, new List<(int, int)> { (33,33) });

			//yield return new GetRangeTestCase((25, 40), (30, 50), 0L, new List<(int, int)> { (30, 40), (41, 50) });
			//yield return new GetRangeTestCase((25, 40), (30, 35), 0L, new List<(int, int)> { (30, 35) });
			//yield return new GetRangeTestCase((25, 40), (10, 30), 0L, new List<(int, int)> { (10, 24), (25, 30) });
			//yield return new GetRangeTestCase((25, 40), (10, 50), 0L, new List<(int, int)> { (10, 24), (25, 40), (41, 50) });
		}

		public record GetRangeTestCase((int s, int e) source, (int s, int e) range, long offset, List<(int s, int e)> e);
	}
}