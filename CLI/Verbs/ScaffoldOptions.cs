using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CLI.Verbs;

[Verb("scaffold", HelpText = "Create class file for given day & year, with optional unit test")]
public class ScaffoldOptions : BaseOptions
{
	[Option(shortName: 'u', HelpText = "Creates a unit test file")]
	public bool HasUnitTest { get; set; }

	[Option(shortName: 'e', HelpText = "Sets the expected value for example part 1 and reads input from file", SetName = "Single")]
	public string ExpectedValuePart1 { get; set; }

	[Option(shortName: 'c', HelpText = "Sets testcases for example part 1", SetName = "Multiple")]
	public IEnumerable<string> Cases { get; set; }

	private string ClassPath { get; set; }
	private string TestPath { get; set; }

	public List<(string input, string outcome)> TestCases { get; private set; }

	public override (bool isValid, string message) Validate()
	{
		var result = base.Validate();

		if (!result.isValid) return result;

		var classDir = $"{RootPath}\\AoC{Year}";
		ClassPath = $"{classDir}\\Day{DayString}.cs";
		var testDir = $"{RootPath}\\AoC{Year}Tests";
		TestPath = $"{testDir}\\Day{DayString}Test.cs";

		if (!Directory.Exists(classDir))
			return (false, $"Error: project for year {Year} could not be found at {classDir}.");

		if (File.Exists(ClassPath))
			return (false, $"Error: file for day {Day} year {Year} already exists at {ClassPath}.");

		if (!HasUnitTest) return result;

		if (!Directory.Exists(testDir))
			return (false, $"Error: test project for year {Year} could not be found at {testDir}.");

		if (File.Exists(TestPath))
			return (false, $"Error: test for day {Day} year {Year} already exists at {TestPath}.");

		if (Cases == null) return result;

		var valid = Cases.All(c => c.Contains(":"));
		result = (valid, valid ? string.Empty : $"Error: test cases must be in format input:outcome");

		if (!valid) return result;

		TestCases = Cases.Select(c => c.Split(":")).Select(c => (input: c[0], outcome: c[1])).ToList();
		var allValid = TestCases.All(c => !string.IsNullOrEmpty(c.input) && !string.IsNullOrEmpty(c.outcome));
		result = (allValid, allValid ? string.Empty : $"Error: not all test cases have valid input and outcome");

		return result;
	}

	public static async Task<string> Run(ScaffoldOptions options)
	{
		var (isValid, message) = options.Validate();

		if (isValid)
		{
			var template = new SolutionTemplate
			{
				Year = options.Year,
				Day = options.DayString,
				HasUnitTest = options.HasUnitTest,
				ExpectedValuePart1 = options.ExpectedValuePart1,
				TestCases = options.TestCases,
			};

			await File.WriteAllTextAsync(options.ClassPath, template.CreateSolution());

			if (options.HasUnitTest)
				await File.WriteAllTextAsync(options.TestPath, template.CreateUnitTest());

			message = $"Success: created file for year {options.Year} day {options.Day} with {(options.HasUnitTest ? "additional" : "no")} unit test.";
		}

		Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;

		return message;
	}

	public async Task<GetInputOptions> ToGetInputOption() => new() 
	{
		Year = Year,
		Day = Day,
		SessionId = await File.ReadAllTextAsync(Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly( ).Location).FullName, "aoctoken.txt")) // bit iffy, no validation here..
	};

}