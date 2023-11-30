using AoC2018;

namespace AoC2018Tests
{
	public class Day11Test
	{
		Day11 day11;

		[SetUp]
		public void Setup()
		{
			day11 = new Day11("day11");
		}

		[TestCase(18, "33,45")]
		[TestCase(42, "21,61")]
		public void Part1(int serialNumber, string expected)
		{
			Day11.SerialNumber = serialNumber;
			var actual = day11.SolvePart1( );
			Assert.That(actual, Is.EqualTo(expected));
		}

		[TestCase(18, "90,269,16")]
		[TestCase(42, "232,251,12")]
		public void Part2(int serialNumber, string expected)
		{
			Day11.SerialNumber = serialNumber;
			var actual = day11.SolvePart2( );
			Assert.That(actual, Is.EqualTo(expected));
		}

		[TestCase(3, 5, 8, 4)]
		[TestCase(122, 79, 57, -5)]
		[TestCase(217, 196, 39, 0)]
		[TestCase(101, 153, 71, 4)]
		public void CalculatePowerLevel(int x, int y, int serialNumber, int expected)
		{
			var actual = Day11.CalculatePowerLevel((x, y), serialNumber);
			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}