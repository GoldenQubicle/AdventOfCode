using CommandLine;
using Common;
using System;
using System.IO;
using System.Reflection;

namespace CLI
{
    [Verb("runday", HelpText = "Run a single day")]
    public class RunDayOptions
    {
        [Option(shortName: 'y', Required = true, HelpText = "The Year, from 2015 to 2020")]
        public int Year { get; set; }

        [Option(shortName: 'd', Required = true, HelpText = "The Day, from 1 to 25")]
        public int Day { get; set; }

        [Option(shortName: 'p', Required = true, HelpText = "The Part, 1, 2, or 3 for both parts")]
        public int Part { get; set; }

        public static string Run(RunDayOptions r)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            if ( r.Year < 2015 || r.Year > 2020 )
                return $"Error: year must be between 2015 and 2020.";

            if ( r.Day < 1 || r.Day > 25 )
                return $"Error: day must be between 1 and 25.";

            var dayString = r.Day < 10 ? r.Day.ToString( ).PadLeft(2, '0') : r.Day.ToString( );
            var root = Assembly.GetExecutingAssembly( ).Location.Split("\\CLI")[0];
            var dir = $"{root}\\AoC{r.Year}\\bin\\Debug\\net5.0";
            var path = $"{dir}\\AoC{r.Year}.dll";

            if ( !Directory.Exists(dir) )
                return $"Error: project bin for year {r.Year} cannot be found at {dir}";

            if ( !File.Exists(path) )
                return $"Error: dll file for year {r.Year} cannot be found at {path}";

            var assembly = Assembly.LoadFrom(path);
            var dayType = assembly.GetType($"AoC{r.Year}.Day{dayString}");
            var ctorType = new Type[ ] { typeof(string) };
            var ctor = dayType.GetConstructor(ctorType);
            var dayToRun = ( Solution ) ctor.Invoke(new object[ ] { $"day{dayString}" });

            var message = r.Part switch
            {
                1 => $"Day {dayString} Part 1: {dayToRun.SolvePart1( )}",
                2 => $"Day {dayString} Part 2: {dayToRun.SolvePart2( )}",
                3 => $"Day {dayString} Part 1: {dayToRun.SolvePart1( )} \nDay {dayString} Part 2: {dayToRun.SolvePart2( )}",
                _ => string.Empty
            };

            Console.ForegroundColor = string.IsNullOrEmpty(message) ? ConsoleColor.Red : ConsoleColor.Green;
            return string.IsNullOrEmpty(message) ? "Error: malformed part flag" : $"Year {r.Year}\n{message}";
        }
    }
}
