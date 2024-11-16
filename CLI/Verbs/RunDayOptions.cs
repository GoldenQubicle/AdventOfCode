using CommandLine;
using Common;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CLI.Verbs;

[Verb("runday", HelpText = "Run a single day")]
public class RunDayOptions : BaseOptions
{
	[Option(shortName: 'p', HelpText = "Part one or two, or both if not specified")]
	public int Part { get; set; }
	public Type DayType { get; private set; }

	public override (bool isValid, string message) Validate( )
	{
		var result = base.Validate( );

		if ( !result.isValid ) return result;

		//note we assume debug builds are present
		var dir = $"{RootPath}\\AoC{Year}\\bin\\Debug\\net8.0";
		var assemblyPath = $"{dir}\\AoC{Year}.dll";

		if ( !Directory.Exists(dir) )
			result = (false, $"Error: project bin for year {Year} cannot be found at {dir}");

		if ( !File.Exists(assemblyPath) )
			result = (false, $"Error: dll file for year {Year} cannot be found at {assemblyPath}");

		if ( Part > 2 )
			result = (false, $"Error: malformed part flag");

		if ( !File.Exists($"{RootPath}\\AoC{Year}\\data\\day{DayString}.txt") )
			result = (false, $"Error: could not find input file for day {Day} at {RootPath}\\AoC{Year}\\data");

		// return before loading assembly
		if ( !result.isValid ) return result;

		var assembly = Assembly.LoadFrom(assemblyPath);
		DayType = assembly.GetType($"AoC{Year}.Day{DayString}");

		//final check if day actually exists.. could probably just check the .cs or .fs file..?
		//though could be present while not in dll in case it hasn't build in a while
		return DayType == null ? (false, $"Error: could not find day {Day} for year {Year} in assembly at {assemblyPath}.") : result;
	}

	public static async Task<string> Run(RunDayOptions options)
	{
		var (isValid, message) = options.Validate( );

		if (isValid)
		{
			var partString = options.Part == 0 ? "part 1 & 2" : $"part {options.Part}";
			Console.Write($"Starting to solve {options.Year} day {options.Day} {partString} \n");

			var cts = new CancellationTokenSource( );

			var loggerTask = PeriodicLogger(TimeSpan.FromSeconds(15), cts.Token);
			
			var solutions =  GetSolutions(options);
			
			try
			{
				var(part1, part2) = await solutions;
				message = options.Part switch
				{
					1 => $"Year {options.Year}\nDay {options.DayString} Part 1: {part1}",
					2 => $"Year {options.Year}\nDay {options.DayString} Part 2: {part2}",
					_ => $"Year {options.Year}\nDay {options.DayString} Part 1: {part1} \nDay {options.DayString} Part 2: {part2}",
				};
			} finally
			{
				await cts.CancelAsync( );
				await loggerTask;
			}

			
		}

		Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
		return message;
	}

	private static async Task<(string part1, string part2)> GetSolutions(RunDayOptions options)
	{
		var ctorType = new[ ] { typeof(string) };
		var ctor = options.DayType.GetConstructor(ctorType);

		var part1 = options.Part is 1 or 0
			? await ((Solution)ctor.Invoke([$"day{options.DayString}"])).SolvePart1( )
			: string.Empty;

		var part2 = options.Part is 2 or 0
			? await ((Solution)ctor.Invoke([$"day{options.DayString}"])).SolvePart2( )
			: string.Empty;

		return (part1, part2);
	}

	static async Task PeriodicLogger( TimeSpan interval, CancellationToken cancellationToken)
	{
		while (!cancellationToken.IsCancellationRequested)
		{
			Console.WriteLine($"...the elves are busy busy busy... {DateTime.Now.ToLongTimeString( )}");
			try
			{
				await Task.Delay(interval, cancellationToken);
			} catch (TaskCanceledException)
			{
				// Expected when the token is canceled
				break;
			}
		}
	}
}