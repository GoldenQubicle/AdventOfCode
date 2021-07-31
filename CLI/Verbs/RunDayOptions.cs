using CommandLine;
using Common;
using System;
using System.IO;
using System.Reflection;
//using AoC2019;

namespace CLI.Verbs
{
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
            var dir = $"{RootPath}\\AoC{Year}\\bin\\Debug\\net5.0";
            var assemblyPath = $"{dir}\\AoC{Year}.dll";

            if ( !Directory.Exists(dir) )
                result = (false, $"Error: project bin for year {Year} cannot be found at {dir}");

            if ( !File.Exists(assemblyPath) )
                result = (false, $"Error: dll file for year {Year} cannot be found at {assemblyPath}");

            if ( Part > 2 )
                result = (false, $"Error: malformed part flag");

            if ( !File.Exists($"{RootPath}\\AoC{Year}\\data\\day{DayString}.txt") )
                result = (false, $"Error: could not find input file for day {Day} at {RootPath}\\AoC{Year}\\data\\.");

            // return before loading assembly
                if ( !result.isValid ) return result;

            var assembly = Assembly.LoadFrom(assemblyPath);
            DayType = assembly.GetType($"AoC{Year}.Day{DayString}");

           //final check if day actually exists.. could probably just check the .cs or .fs file..?
           //though could be present while not in dll in case it hasn't build in a while
            return DayType == null ? (false, $"Error: could not find day {Day} for year {Year} in assembly at {assemblyPath}.") : result;
        }

        public static string Run(RunDayOptions options)
        {
            var (isValid, message) = options.Validate( );

            if (isValid)
            {
                var (part1, part2) = GetSolutions(options);

                message = options.Part switch
                {
                    1 => $"Year {options.Year}\nDay {options.DayString} Part 1: {part1}",
                    2 => $"Year {options.Year}\nDay {options.DayString} Part 2: {part2}",
                    _ => $"Year {options.Year}\nDay {options.DayString} Part 1: {part1} \nDay {options.DayString} Part 2: {part2}",
                };
            }

            Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
            return message;
        }

        private static (string part1, string part2) GetSolutions(RunDayOptions options)
        { 
            var ctorType = new[] { typeof(string) };
            var ctor = options.DayType.GetConstructor(ctorType);
            var dayToRun = (Solution)ctor.Invoke(new object[] { $"day{options.DayString}" });
            return (dayToRun.SolvePart1(), dayToRun.SolvePart2());
        }
    }
}
