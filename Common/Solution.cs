namespace Common;

public abstract class Solution
{
	public List<string> Input { get; }

	protected Solution(string file, bool doTrim = true, bool doRemoveEmptyLines = true) =>
		Input = ParseInput(file, "\r\n", doTrim, doRemoveEmptyLines);

	protected Solution(string file, string split, bool doTrim = true, bool doRemoveEmptyLines = true) =>
		Input = ParseInput(file, split, doTrim, doRemoveEmptyLines);

	protected Solution(List<string> input) => Input = input;
	protected Solution() { }

	private const string y2017 = "2017";

	private List<string> ParseInput(string file, string split, bool doTrim, bool doRemoveEmptyLines)
	{
		// Admittedly bit hacky wacky solution, however, the solution MUST be able to find the correct data folder regardless where it was called from - (common) tests, CLI, SharpRay, etc
		var aocYear = GetType().FullName.Split('.')[0].Where(char.IsDigit).AsString();
		var dir = GetAocDebugDirectory(aocYear);
		var lines = File.ReadAllText($"{dir}\\data\\{file}.txt")
			.Split(aocYear == y2017 ? "\n" : split) // for some reason I always manually adjusted the line ends.. lets not do that anymore, and also not break anything
			.Select(s => doTrim ? s.Trim() : s);

		return doRemoveEmptyLines ? lines.Where(s => !string.IsNullOrEmpty(s)).ToList() : lines.ToList();

	}

	public abstract Task<string> SolvePart1();
	public abstract Task<string> SolvePart2();

	/// <summary>
	/// Initializes the solution for the given day and year. Initially used by SharpRay for visualization but also used internally for running tests on all actual results (2020 so far). 
	/// </summary>
	/// <param name="year"></param>
	/// <param name="day">Just the numeric value as string with a leading zero below 10, e.g. "05", "17"</param>
	/// <returns></returns>
	public static Solution Initialize(string year, string day)
	{
		var dir = GetAocDebugDirectory(year);
		var assemblyPath = $"{dir}\\AoC{year}.dll";
		var assembly = Assembly.LoadFrom(assemblyPath);
		var type = assembly.GetType($"AoC{year}.Day{day}");
		var ctorType = new[ ] { typeof(string) };
		var ctor = type.GetConstructor(ctorType);
		var solution = ((Solution)ctor.Invoke(new object[] { $"day{day}" }));
		return solution;
	}

	private static string GetAocDebugDirectory(string year)
	{
		//Assuming a ./source/repos/... folder structure which contains the AoC project, SharpRay, etc
		var root = Directory.GetCurrentDirectory( ).Split("repos")[0];
		return $@"{root}repos\AdventOfCode\AoC{year}\bin\Debug\net8.0";
	}
}