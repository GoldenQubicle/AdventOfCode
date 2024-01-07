using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Common.Interfaces;
using System.Threading.Tasks;

namespace Common;

public abstract class Solution
{
	public Func<IEnumerable<INode>, Task> RenderAction { get; set; }
	public List<string> Input { get; }

	protected Solution(string file, bool doTrim = true, bool doRemoveEmptyLines = true) =>
		Input = ParseInput(file, "\r\n", doTrim, doRemoveEmptyLines);

	protected Solution(string file, string split, bool doTrim = true, bool doRemoveEmptyLines = true) =>
		Input = ParseInput(file, split, doTrim, doRemoveEmptyLines);

	protected Solution(List<string> input) => Input = input;
	protected Solution() { }

	private const string y2017 = "AoC2017";

	private List<string> ParseInput(string file, string split, bool doTrim, bool doRemoveEmptyLines)
	{
		// admittedly bit hacky solution 
		// however the solution MUST be able to find the correct data folder,
		// whether it was called from a test, the cli or whatever. 
		var aocYear = GetType().FullName.Split('.')[0];
		var path = GetType().Assembly.Location.Replace($"{aocYear}.dll", "");
		var lines = File.ReadAllText($"{path}\\data\\{file}.txt")
			.Split(aocYear == y2017 ? "\n" : split) // for some reason I always manually adjusted the line ends.. lets not do that anymore, and also not break anything
			.Select(s => doTrim ? s.Trim() : s);

		return doRemoveEmptyLines ? lines.Where(s => !string.IsNullOrEmpty(s)).ToList() : lines.ToList();

	}

	public abstract Task<string> SolvePart1();
	public abstract Task<string> SolvePart2();

	/// <summary>
	/// Initializes the solution for the given day and year. Initially used by SharpRay for visualization but also used internally for 2020. 
	/// </summary>
	/// <param name="year"></param>
	/// <param name="day">Just the numeric value as string with a leading zero below 10, e.g. "05", "17"</param>
	/// <returns></returns>
	public static Solution Initialize(string year, string day)
	{
		//Assuming a ./source/repos/... folder structure which contains the AoC project, and SharpRay
		var root = Directory.GetCurrentDirectory( ).Split("repos")[0];
		var dir = $"{root}repos\\AdventOfCode\\AoC{year}\\bin\\Debug\\net8.0";
		var assemblyPath = $"{dir}\\AoC{year}.dll";
		var assembly = Assembly.LoadFrom(assemblyPath);
		var type = assembly.GetType($"AoC{year}.Day{day}");
		var ctorType = new[ ] { typeof(string) };
		var ctor = type.GetConstructor(ctorType);
		var solution = ((Solution)ctor.Invoke(new object[] { $"day{day}" }));
		return solution;
	}
}