using System.Runtime.CompilerServices;
using Common.Extensions;
using NUnit.Framework.Internal;

namespace AoC2019Tests;

internal class IntCodeComputerTests
{
	[TestCaseSource(nameof(GetDay2TestCases))]
	public void Day2Tests((List<long> input, List<long> result) test)
	{
		var sut = new IntCodeComputer(test.input);
		sut.Execute( );

		Assert.That(sut.Memory, Is.EqualTo(test.result));
	}

	[TestCaseSource(nameof(GetDay5Part1TestCases))]
	public void Day5Part1Tests((List<long> input, List<long> result) test)
	{
		var sut = new IntCodeComputer(test.input);
		sut.Execute( );

		Assert.That(sut.Memory.Take(test.result.Count), Is.EqualTo(test.result));
	}

	[TestCaseSource(nameof(GetDay5Part2TestCases))]
	public void Day5Part2Tests((int input, List<long> program, long result) test)
	{
		var sut = new IntCodeComputer(test.program) { Inputs = new(){ test.input }};
		sut.Execute( );
		Assert.That(sut.Output, Is.EqualTo(test.result));
	}

	[TestCaseSource(nameof(GetDay7Part1TestCases))]
	public async Task Day7Part1Tests((string input, string result) test)
	{
		var day07 = new Day07(test.input);
		var actual = await day07.SolvePart1();
		Assert.That(actual, Is.EqualTo(test.result));
	}

	[TestCaseSource(nameof(GetDay7Part2TestCases))]
	public async Task Day7Part2Tests((string input, string result) test)
	{
		var day07 = new Day07(test.input);
		var actual = await day07.SolvePart2( );
		Assert.That(actual, Is.EqualTo(test.result));
	}

	[TestCaseSource(nameof(GetDay9TestCases))]
	public async Task Day9Part1Tests((List<long> program, long result) test)
	{
		var sut = new IntCodeComputer(test.program);
		sut.Execute();		
		Assert.That(sut.Output, Is.EqualTo(test.result));
	}

	public static IEnumerable<(List<long> program, long result)> GetDay9TestCases()
	{
		yield return (new(){ 104, 1125899906842624, 99 }, 1125899906842624);
		yield return (new(){ 1102, 34915192, 34915192, 7, 4, 7, 99, 0 }, 1219070632396864);
		yield return (new(){ 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 }, 99 );
	}

	public static IEnumerable<(string input, string result)> GetDay7Part2TestCases()
	{
		yield return ("day07test4", "139629729");
		yield return ("day07test5", "18216");
	}

	public static IEnumerable<(string input, string result)> GetDay7Part1TestCases()
	{
		yield return ("day07test1", "43210");
		yield return ("day07test2", "54321");
		yield return ("day07test3", "65210");
	}

	public static IEnumerable<(int input, List<long> program, long result)> GetDay5Part2TestCases()
	{
		yield return (8, new( ) { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 1);
		yield return (7, new( ) { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 0);
		yield return (7, new( ) { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 1);
		yield return (8, new( ) { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 0);
		yield return (8, new( ) { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 1);
		yield return (7, new( ) { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 0);
		yield return (7, new( ) { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 1);
		yield return (8, new( ) { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 0);
		yield return (0, new( ) { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 0);
		yield return (1, new( ) { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 1);
		yield return (0, new( ) { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 0);
		yield return (1, new( ) { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 1);
		yield return (7, new( ) { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 999);
		yield return (8, new( ) { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 1000);
		yield return (9, new( ) { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 1001);
	}

	public static IEnumerable<(List<long> input, List<long> result)> GetDay5Part1TestCases()
	{
		yield return (new( ) { 1002, 4, 3, 4, 33 }, new( ) { 1002, 4, 3, 4, 99 });
		yield return (new( ) { 1101, 100, -1, 4, 0 }, new( ) { 1101, 100, -1, 4, 99 });
		yield return (new( ) { 11102, 4, 4, 0, 99 }, new( ) { 16, 4, 4, 0, 99 });
		yield return (new( ) { 11101, 4, 4, 0, 99 }, new( ) { 8, 4, 4, 0, 99 });
	}

	public static IEnumerable<(List<long> input, List<long> result)> GetDay2TestCases()
	{
		yield return (new( ) { 1, 0, 0, 0, 99 }, new( ) { 2, 0, 0, 0, 99 });
		yield return (new( ) { 2, 3, 0, 3, 99 }, new( ) { 2, 3, 0, 6, 99 });
		yield return (new( ) { 2, 4, 4, 5, 99, 0 }, new( ) { 2, 4, 4, 5, 99, 9801 });
		yield return (new( ) { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new( ) { 30, 1, 1, 4, 2, 5, 6, 0, 99 });
	}

	[Test]
	public async Task SolutionDay2()
	{
		var day2 = new Day02("day02");
		var part1 = await day2.SolvePart1( );
		var part2 = await day2.SolvePart2( );

		Assert.That(part1, Is.EqualTo("3790645"));
		Assert.That(part2, Is.EqualTo("6577"));
	}

	[Test]
	public async Task SolutionDay5()
	{
		var day5 = new Day05("day05");
		var part1 = await day5.SolvePart1( );
		var part2 = await day5.SolvePart2( );

		Assert.That(part1, Is.EqualTo("7566643"));
		Assert.That(part2, Is.EqualTo("9265694"));
	}

	[Test]
	public async Task SolutionDay7()
	{
		var day7 = new Day07("day07");
		var part1 = await day7.SolvePart1( );
		var part2 = await day7.SolvePart2( );

		Assert.That(part1, Is.EqualTo("880726"));
		Assert.That(part2, Is.EqualTo("4931744"));
	}

	[Test]
	public async Task SolutionDay9()
	{
		var day9 = new Day09("day09");
		var part1 = await day9.SolvePart1( );
		var part2 = await day9.SolvePart2( );

		Assert.That(part1, Is.EqualTo("3429606717"));
		Assert.That(part2, Is.EqualTo("33679"));
	}

	[Test]
	public async Task SolutionDay11()
	{
		var day11 = new Day11("day11");
		var part1 = await day11.SolvePart1( );
		var part2 = await day11.SolvePart2( );

		Assert.That(part1, Is.EqualTo("2184"));
		Assert.That(part2, Is.EqualTo("\n..##..#..#..##..#..#.####.####.###..#..#...\n.#..#.#..#.#..#.#..#....#.#....#..#.#.#....\n.#..#.####.#....####...#..###..#..#.##.....\n.####.#..#.#....#..#..#...#....###..#.#....\n.#..#.#..#.#..#.#..#.#....#....#....#.#....\n.#..#.#..#..##..#..#.####.####.#....#..#...\n"));
	}
}