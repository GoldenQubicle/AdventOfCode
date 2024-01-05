using AoC2018;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AoC2018Tests
{
	public class Day14Test
	{
		Day14 day14;

		[SetUp]
		public void Setup()
		{
			day14 = new Day14("day14test1");
		}

		[TestCase(9, "5158916779")]
		[TestCase(5, "0124515891")]
		[TestCase(18, "9251071085")]
		[TestCase(2018, "5941429882")]
		public void Part1(int make, string expected)
		{
			Day14.Make = make;
			var actual = day14.SolvePart1( ).Result;
			Assert.That(actual, Is.EqualTo(expected));
		}

		[TestCaseSource(nameof(GetTestCases))]
		public void Part2(TestCase test)
		{
			Day14.LookFor = test.lookFor;
			var actual = day14.SolvePart2( ).Result;
			Assert.That(actual, Is.EqualTo(test.expected.ToString( )));
		}

		public record TestCase(List<int> lookFor, int expected);

		private static IEnumerable<TestCase> GetTestCases()
		{
			yield return new TestCase(new( ) { 5, 1, 5, 8, 9 }, 9);
			yield return new TestCase(new( ) { 0, 1, 2, 4, 5 }, 5);
			yield return new TestCase(new( ) { 9, 2, 5, 1, 0 }, 18);
			yield return new TestCase(new( ) { 5, 9, 4, 1, 4 }, 2018);
		}
	}
}