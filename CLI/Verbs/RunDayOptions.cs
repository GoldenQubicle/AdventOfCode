using CommandLine;
using Common;
using System;
using System.IO;
using System.Reflection;

namespace CLI.Verbs
{
    [Verb("runday", HelpText = "Run a single day")]
    public class RunDayOptions : BaseOptions
    {
        [Option(shortName: 'p', HelpText = "Part one or two, or both if not specified")]
        public int Part { get; set; }
        public Type DayType { get; private set; }

        public override (bool IsValid, string message) Validate( )
        {
            var result = base.Validate( );

            if ( !result.IsValid ) return result;
            //note we assume debug builds are present
            var dir = $"{RootPath}\\AoC{Year}\\bin\\Debug\\net5.0";
            var assemblyPath = $"{dir}\\AoC{Year}.dll";

            if ( !Directory.Exists(dir) )
                result = (false, $"Error: project bin for year {Year} cannot be found at {dir}");

            if ( !File.Exists(assemblyPath) )
                result = (false, $"Error: dll file for year {Year} cannot be found at {assemblyPath}");

            if ( Part > 2 )
                result = (false, $"Error: malformed part flag");

            // return before loading assembly
            if ( !result.IsValid ) return result;

            var assembly = Assembly.LoadFrom(assemblyPath);
            DayType = assembly.GetType($"AoC{Year}.Day{DayString}");
            //final check if day actually exists.. could probably just check the .cs file..?
            //though could be present while not in dll in case it hasn't build in a while
            if ( DayType == null )
                result = (false, $"Error: could not find day {Day} for year {Year} in assembly at {assemblyPath}.");

            return result;
        }

        public static string Run(RunDayOptions options)
        {
            var (isValid, message) = options.Validate( );

            if ( isValid )
            {
                var ctorType = new Type[ ] { typeof(string) };
                var ctor = options.DayType.GetConstructor(ctorType);
                var dayToRun = ( Solution ) ctor.Invoke(new object[ ] { $"day{options.DayString}" });

                message = options.Part switch
                {
                    1 => $"Year {options.Year}\nDay {options.DayString} Part 1: {dayToRun.SolvePart1( )}",
                    2 => $"Year {options.Year}\nDay {options.DayString} Part 2: {dayToRun.SolvePart2( )}",
                    _ => $"Year {options.Year}\nDay {options.DayString} Part 1: {dayToRun.SolvePart1( )} \nDay {options.DayString} Part 2: {dayToRun.SolvePart2( )}",
                };
            }

            Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
            return message;
        }
    }
}
