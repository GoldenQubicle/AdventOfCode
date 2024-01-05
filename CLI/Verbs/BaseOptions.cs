using CommandLine;
using System.Reflection;

namespace CLI.Verbs;

public abstract class BaseOptions
{
	[Option(shortName: 'y', Required = true, HelpText = "The year, from 2015 to 2023")]
	public int Year { get; set; }

	[Option(shortName: 'd', Required = true, HelpText = "The day, from 1 to 25")]
	public int Day { get; set; }

	public string DayString => Day < 10 ? Day.ToString("00") : Day.ToString( );

	public virtual (bool isValid, string message) Validate( )
	{
		if ( Year is < 2015 or > 2023 )
			return (false, $"Error: year must be between 2015 and 2023.");

		if ( Day is < 1 or > 25 )
			return (false, $"Error: day must be between 1 and 25.");

		return (true, string.Empty);
	}

	public static string RootPath => Assembly.GetExecutingAssembly( ).Location.Split("\\CLI")[0];
}